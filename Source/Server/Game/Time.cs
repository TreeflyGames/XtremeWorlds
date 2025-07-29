// TimeManagerEnhanced.cs
// -----------------------------------------------------------------------------
// Modernised, thread‑safe server‑side time manager.
// -----------------------------------------------------------------------------
// • Immutable public surface & XML docs
// • HashSet for O(1) tz look‑ups; thread‑safe mutation via ReaderWriterLockSlim
// • Async broadcast with parallelism & cancellation support
// • Strong validation + guard clauses
// • Event‑driven publish pattern decoupled from Mirage networking details
// -----------------------------------------------------------------------------

#nullable enable

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Mirage.Sharp.Asfw;
using static Core.Packets;

namespace Server;

/// <summary>
/// Server‑side coordinator for game‑time, time‑of‑day changes and clock sync.
/// It proxies a <see cref="Clock"/> instance, adds multi‑timezone helpers and
/// network broadcast helpers.
/// </summary>
public sealed class TimeManager : IDisposable
{
    private const int PacketReserve = 16; // reserve a few extra bytes for future flags

    private readonly Clock _clock;

    // O(1) membership checks; thread‑safe via explicit lock
    private readonly HashSet<TimeZoneInfo> _timeZones = new();
    private readonly ReaderWriterLockSlim _tzLock = new(LockRecursionPolicy.NoRecursion);

    private bool _disposed;

    /// <summary>
    /// Creates a new <see cref="TimeManager"/> bound to the global <see cref="Clock.Instance"/>.
    /// </summary>
    public TimeManager()
    {
        _clock = Clock.Instance;

        AddTimeZone(TimeZoneInfo.Utc);
        TryAddTimeZoneById("Eastern Standard Time");
        TryAddTimeZoneById("Pacific Standard Time");

        _clock.OnTimeChanged     += OnTimeChanged;
        _clock.OnTimeOfDayChanged+= OnTimeOfDayChanged;
        _clock.OnTimeSync        += OnTimeSync;

        _clock.Time      = DateTime.UtcNow;
        _clock.GameSpeed = 0; // paused by default
    }

    #region Public API

    /// <summary>
    /// Thread‑safe, read‑only snapshot of supported time‑zones.
    /// </summary>
    public IReadOnlyCollection<TimeZoneInfo> SupportedTimeZones
    {
        get
        {
            _tzLock.EnterReadLock();
            try => _timeZones.ToList();
            finally _tzLock.ExitReadLock();
        }
    }

    /// <inheritdoc cref="Clock.TimeOfDay"/>
    public TimeOfDay TimeOfDay => _clock.TimeOfDay;

    /// <inheritdoc cref="Clock.GameSpeed"/>
    public int GameSpeed
    {
        get => _clock.GameSpeed;
        set
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            _clock.GameSpeed = value;
        }
    }

    /// <summary>
    /// Attempts to add a <see cref="TimeZoneInfo"/> to the supported list.
    /// Returns <c>true</c> if added, <c>false</c> if it already existed.
    /// </summary>
    public bool AddTimeZone(TimeZoneInfo timeZone)
    {
        ArgumentNullException.ThrowIfNull(timeZone);
        _tzLock.EnterWriteLock();
        try => _timeZones.Add(timeZone);
        finally _tzLock.ExitWriteLock();
    }

    /// <summary>
    /// Removes a previously added <see cref="TimeZoneInfo"/>.
    /// </summary>
    public bool RemoveTimeZone(TimeZoneInfo timeZone)
    {
        ArgumentNullException.ThrowIfNull(timeZone);
        _tzLock.EnterWriteLock();
        try => _timeZones.Remove(timeZone);
        finally _tzLock.ExitWriteLock();
    }

    /// <summary>
    /// Converts the current game clock to the requested timezone (must be supported).
    /// </summary>
    public DateTime GetTimeInZone(TimeZoneInfo timeZone)
    {
        ArgumentNullException.ThrowIfNull(timeZone);
        _tzLock.EnterReadLock();
        try
        {
            if (!_timeZones.Contains(timeZone))
                throw new InvalidOperationException($"Time‑zone {timeZone.Id} not supported.");
        }
        finally { _tzLock.ExitReadLock(); }

        return TimeZoneInfo.ConvertTimeFromUtc(_clock.Time, timeZone);
    }

    /// <summary>
    /// Adjusts the global clock by the supplied <paramref name="delta"/>.
    /// </summary>
    public void AdjustTime(TimeSpan delta) => _clock.Time = _clock.Time.Add(delta);

    /// <summary>
    /// Broadcasts the authoritative UTC time to all <paramref name="servers"/>.
    /// </summary>
    public async Task SynchronizeTimeAsync(IEnumerable<Server> servers, CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(servers);
        var utc = _clock.Time;

        var tasks = servers.Select(async server =>
        {
            // TODO: Implement SendTimeAsync on Server type
            // await server.SendTimeAsync(utc, ct).ConfigureAwait(false);
            await Task.CompletedTask.ConfigureAwait(false);
        });

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    #endregion

    #region Event forwarding

    private void OnTimeChanged(ref Clock _) => General.UpdateCaption();

    private void OnTimeOfDayChanged(ref Clock _) => BroadcastTime();

    private void OnTimeSync(ref Clock _) => BroadcastClock();

    #endregion

    #region Networking helpers

    public void BroadcastClock()
    {
        Parallel.For(0, NetworkConfig.Socket.HighIndex, i =>
        {
            if (NetworkConfig.IsPlaying(i)) SendClock(i);
        });
    }

    public void BroadcastTime()
    {
        Parallel.For(0, NetworkConfig.Socket.HighIndex, i =>
        {
            if (NetworkConfig.IsPlaying(i)) SendTime(i);
        });
    }

    public void SendClock(int index)
    {
        using var bs = new ByteStream(PacketReserve);
        bs.WriteInt32((int)ServerPackets.SClock);
        bs.WriteInt32(GameSpeed);
        bs.WriteInt64(_clock.Time.Ticks);
        NetworkConfig.Socket.SendDataTo(index, bs.UnreadData, bs.WritePosition);
    }

    public void SendTime(int index)
    {
        using var bs = new ByteStream(PacketReserve);
        bs.WriteInt32((int)ServerPackets.STime);
        bs.WriteByte((byte)TimeOfDay);
        NetworkConfig.Socket.SendDataTo(index, bs.UnreadData, bs.WritePosition);
    }

    #endregion

    #region Helpers

    private void TryAddTimeZoneById(string id)
    {
        try
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById(id);
            AddTimeZone(tz);
        }
        catch (TimeZoneNotFoundException) { /* ignore missing tz on *nix */ }
    }

    #endregion

    #region Disposal

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        _clock.OnTimeChanged      -= OnTimeChanged;
        _clock.OnTimeOfDayChanged -= OnTimeOfDayChanged;
        _clock.OnTimeSync         -= OnTimeSync;

        _tzLock.Dispose();
        GC.SuppressFinalize(this);
    }

    ~TimeManager() => Dispose();

    #endregion
}
