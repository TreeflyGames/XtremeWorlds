using System;
using System.IO;
using Core;
using static Core.Global.Command;
using ManagedBass;
using ManagedBass.Midi;
using Microsoft.VisualBasic.CompilerServices;

namespace Client
{

    public class Sound
    {

        // Sound and Music handles for ManagedBass
        public static int MusicStream;
        public static int SoundStream;
        public static int WeatherStream;
        public static int ExtraSoundStream;

        public static string[] MusicCache;
        public static string[] SoundCache;

        public static bool FadeInSwitch;
        public static bool FadeOutSwitch;
        public static string CurrentMusic;
        public static string CurrentWeatherMusic;

        public static int SoundFontHandle;

        public static void PlayMusic(string fileName)
        {
            string path = System.IO.Path.Combine(Core.Path.Music, fileName);

            if (fileName == "None")
            {
                return;
            }

            if (Conversions.ToInteger(SettingsManager.Instance.Music) == 0 | !File.Exists(path))
            {
                StopMusic();
                return;
            }

            if ((fileName ?? "") == (CurrentMusic ?? ""))
            {
                return;
            }

            if (System.IO.Path.GetExtension(fileName).ToLower() == ".mid")
            {
                StopMusic();
                PlayMidi(path);
                CurrentMusic = fileName;
                return;
            }

            try
            {
                StopMusic(); // Stop any currently playing music before starting a new one

                MusicStream = Bass.CreateStream(path, 0L, 0L, BassFlags.Loop);
                if (MusicStream != 0)
                {
                    Bass.ChannelPlay(MusicStream);
                    Bass.ChannelSetAttribute(MusicStream, ChannelAttribute.Volume, SettingsManager.Instance.MusicVolume / 100.0f);
                    CurrentMusic = fileName;
                    FadeInSwitch = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing music: {ex.Message}");
            }
        }

        public static void InitializeBASS()
        {
            // Initialize BASS with the default output device
            if (!Bass.Init(-1, 44100, DeviceInitFlags.Default))
            {
                Console.WriteLine($"Failed to initialize BASS. Error: {Bass.LastError}");
                return;
            }

            // Load the SoundFont (.sf2) for MIDi playback
            string soundFontPath = "GeneralUser.sf2";
            if (!File.Exists(soundFontPath))
            {
                Console.WriteLine($"SoundFont not found: {soundFontPath}");
                return;
            }

            // Initialize the SoundFont
            SoundFontHandle = BassMidi.FontInit(soundFontPath, (FontInitFlags)BassFlags.Default);
            if (SoundFontHandle == 0)
            {
                Console.WriteLine($"Failed to load SoundFont. Error: {Bass.LastError}");
                return;
            }

            // Set the volume for the SoundFont
            BassMidi.FontSetVolume(SoundFontHandle, 1.0f); // 100% volume

            General.CacheMusic();
            General.CacheSound();
        }

        public static void PlayMidi(string filePath)
        {
            StopMusic(); // Ensure previous music is stopped

            // Load and play the MIDi file using ManagedBass.Midi
            MusicStream = BassMidi.CreateStream(filePath, 0L, 0L, BassFlags.Loop, 44100);

            // Correctly set the SoundFont for the MIDi stream
            var font = new MidiFont()
            {
                Handle = SoundFontHandle, // Use the SoundFont handle
                Preset = -1, // -1 means all presets from the SoundFont
                Bank = 0 // Bank 0 (General MIDi bank)
            };

            // Create an array with the MidiFont structure
            MidiFont[] fonts = new MidiFont[] { font };

            // Set the fonts for the MIDi stream and check if it succeeded
            int fontCount = BassMidi.StreamSetFonts(MusicStream, fonts, fonts.Length);

            if (fontCount == 0)
            {
                Console.WriteLine($"Failed to assign SoundFont. Error: {Bass.LastError}");
            }

            // Ensure the file exists before attempting to load it
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"MIDi file not found: {filePath}");
                return;
            }

            if (MusicStream != 0)
            {
                Bass.ChannelPlay(MusicStream);
                Bass.ChannelSetAttribute(MusicStream, ChannelAttribute.Volume, SettingsManager.Instance.MusicVolume / 100.0f);
            }
            else
            {
                // Log the last error if stream creation fails
                var errorCode = Bass.LastError;
                Console.WriteLine($"Failed to load MIDi file. Error: {errorCode}");
            }
        }

        public static void StopMusic()
        {
            if (MusicStream != 0)
            {
                Bass.ChannelStop(MusicStream);
                Bass.StreamFree(MusicStream);
                MusicStream = 0;
                CurrentMusic = "";
            }
        }

        public static void PlaySound(string fileName, int x, int y, bool looped = false)
        {
            if (Conversions.ToInteger(SettingsManager.Instance.Sound) == 0 | !File.Exists(Core.Path.Sounds + fileName))
                return;

            try
            {
                StopSound(); // Stop previous sound if any

                SoundStream = Bass.CreateStream(Core.Path.Sounds + fileName, 0L, 0L, looped ? BassFlags.Loop : BassFlags.Default);
                if (SoundStream != 0)
                {
                    double calculatedVolume = CalculateSoundVolume(ref x, ref y);
                    Bass.ChannelSetAttribute(SoundStream, ChannelAttribute.Volume, calculatedVolume / 100.0d);
                    Bass.ChannelPlay(SoundStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load sound: {ex.Message}");
            }
        }

        public static void StopSound()
        {
            if (SoundStream != 0)
            {
                Bass.ChannelStop(SoundStream);
                Bass.StreamFree(SoundStream);
                SoundStream = 0;
            }
        }

        public static void PlayExtraSound(string fileName, bool looped = false)
        {
            if (Conversions.ToInteger(SettingsManager.Instance.Sound) == 0 | !File.Exists(Core.Path.Sounds + fileName))
                return;

            try
            {
                StopExtraSound();

                ExtraSoundStream = Bass.CreateStream(Core.Path.Sounds + fileName, 0L, 0L, looped ? BassFlags.Loop : BassFlags.Default);
                if (ExtraSoundStream != 0)
                {
                    Bass.ChannelSetAttribute(ExtraSoundStream, ChannelAttribute.Volume, SettingsManager.Instance.SoundVolume / 100.0f);
                    Bass.ChannelPlay(ExtraSoundStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load extra sound: {ex.Message}");
            }
        }

        public static void StopExtraSound()
        {
            if (ExtraSoundStream != 0)
            {
                Bass.ChannelStop(ExtraSoundStream);
                Bass.StreamFree(ExtraSoundStream);
                ExtraSoundStream = 0;
            }
        }

        // The fade methods should directly adjust the volume of the ManagedBass streams
        public static void FadeIn()
        {
            if (MusicStream != 0)
            {
                float currentVolume;
                Bass.ChannelGetAttribute(MusicStream, ChannelAttribute.Volume, out currentVolume);
                if (currentVolume < SettingsManager.Instance.MusicVolume / 100.0f)
                {
                    Bass.ChannelSetAttribute(MusicStream, ChannelAttribute.Volume, currentVolume + 0.03f);
                }
                else
                {
                    FadeInSwitch = false;
                }
            }
        }

        public static void FadeOut()
        {
            if (MusicStream != 0)
            {
                float currentVolume;
                Bass.ChannelGetAttribute(MusicStream, ChannelAttribute.Volume, out currentVolume);
                if (currentVolume > 0f)
                {
                    Bass.ChannelSetAttribute(MusicStream, ChannelAttribute.Volume, currentVolume - 0.03f);
                }
                else
                {
                    FadeOutSwitch = false;
                    StopMusic();
                }
            }
        }

        public static double CalculateSoundVolume(ref int x, ref int y)
        {
            double CalculateSoundVolumeRet = default;
            int x1, x2, y1, y2;
            double Distance;

            if (!(GameState.InGame == true))
            {
                CalculateSoundVolumeRet = 1d;
                return CalculateSoundVolumeRet;
            }

            if (GameState.InGame && x == GetPlayerX(GameState.MyIndex) && y == GetPlayerY(GameState.MyIndex))
            {
                CalculateSoundVolumeRet = 1d;
                CalculateSoundVolumeRet *= SettingsManager.Instance.SoundVolume;
                return CalculateSoundVolumeRet;
            }

            if (x > -1 || y > -1)
            {
                if (x == -1)
                    x = 0;
                if (y == -1)
                    y = 0;
                x1 = Core.Data.Player[GameState.MyIndex].X;
                y1 = Core.Data.Player[GameState.MyIndex].Y;
                x2 = x * 32;
                y2 = y * 32;

                if ((int)Math.Round(Math.Pow(x2 - x1, 2d)) + (int)Math.Round(Math.Pow(y2 - y1, 2d)) < 0)
                {
                    Distance = Math.Sqrt((int)Math.Round(Math.Pow(x2 - x1, 2d)) + (int)Math.Round(Math.Pow(y2 - y1, 2d)) * -1);
                }
                else
                {
                    Distance = Math.Sqrt((int)Math.Round(Math.Pow(x2 - x1, 2d)) + (int)Math.Round(Math.Pow(y2 - y1, 2d)));
                }

                // If the range is greater than 32, do not send a sound
                if (Distance / 32d > 32d)
                {
                    CalculateSoundVolumeRet = 0d;
                }
                else
                {
                    CalculateSoundVolumeRet = 1d / (Distance / 32d);

                    if (CalculateSoundVolumeRet > 1d)
                    {
                        CalculateSoundVolumeRet = 1d;
                    }
                    else if (CalculateSoundVolumeRet < 0d)
                    {
                        CalculateSoundVolumeRet *= -1;
                    }
                }
            }
            else
            {
                CalculateSoundVolumeRet = 1d;
            }

            CalculateSoundVolumeRet *= SettingsManager.Instance.SoundVolume;

            return CalculateSoundVolumeRet;
        }

        public static void FreeBASS()
        {
            if (SoundFontHandle != 0)
            {
                BassMidi.FontFree(SoundFontHandle);
            }
            Bass.Free();
        }

        public static void PlayWeatherSound(string fileName, bool looped = false)
        {
            // Check if sound is enabled and the file exists
            if (!(Conversions.ToInteger(SettingsManager.Instance.Sound) == 1) | !File.Exists(Core.Path.Sounds + fileName))
                return;

            // Avoid reloading the same sound if it's already playing
            if ((CurrentWeatherMusic ?? "") == (fileName ?? ""))
                return;

            // Stop any previously playing weather sound
            StopWeatherSound();

            // Load the new sound file
            string soundPath = Core.Path.Sounds + fileName;
            WeatherStream = Bass.CreateStream(soundPath, 0L, 0L, looped ? BassFlags.Loop : BassFlags.Default);

            // Check if the stream was created successfully
            if (WeatherStream != 0)
            {
                // Play the sound
                Bass.ChannelSetAttribute(WeatherStream, ChannelAttribute.Volume, SettingsManager.Instance.SoundVolume / 100.0f);
                Bass.ChannelPlay(WeatherStream, false);
                CurrentWeatherMusic = fileName;
            }
            else
            {
                Console.WriteLine("Failed to play sound: " + Bass.LastError.ToString());
            }
        }

        public static void StopWeatherSound()
        {
            // Stop and free the current weather sound channel
            if (WeatherStream != 0)
            {
                Bass.ChannelStop(WeatherStream);
                Bass.StreamFree(WeatherStream);
                WeatherStream = 0;
            }

            CurrentWeatherMusic = "";
        }

    }
}