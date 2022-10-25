using System;
using System.IO;

namespace Mirage.Sharp.Asfw
{
  public static class OS
  {
    private static bool _initialized;
    private static bool _isMobile;
    private static bool _isXBox;
    private static bool _isWindows;
    private static bool _isMac;
    private static bool _isLinux;
    private static bool _isBit64;

    private static void Activate()
    {
      if (OS._initialized)
        return;
      if (IntPtr.Size == 8)
        OS._isBit64 = true;
      switch (Environment.OSVersion.Platform)
      {
        case PlatformID.Win32S:
        case PlatformID.Win32Windows:
        case PlatformID.Win32NT:
        case PlatformID.WinCE:
          OS._isWindows = true;
          break;
        case PlatformID.Unix:
          if (Directory.Exists("/Applications") && Directory.Exists("/System") && Directory.Exists("/Users") && Directory.Exists("/Volumes"))
          {
            OS._isMac = true;
            break;
          }
          OS._isLinux = true;
          break;
        case PlatformID.Xbox:
          OS._isXBox = true;
          break;
        case PlatformID.MacOSX:
          OS._isMac = true;
          break;
        default:
          OS._isMobile = true;
          break;
      }
      OS._initialized = true;
    }

    public static bool Mobile
    {
      get
      {
        OS.Activate();
        return OS._isMobile;
      }
    }

    public static bool XBox
    {
      get
      {
        OS.Activate();
        return OS._isXBox;
      }
    }

    public static bool Windows
    {
      get
      {
        OS.Activate();
        return OS._isWindows;
      }
    }

    public static bool Mac
    {
      get
      {
        OS.Activate();
        return OS._isMac;
      }
    }

    public static bool Linux
    {
      get
      {
        OS.Activate();
        return OS._isLinux;
      }
    }

    public static bool X86
    {
      get
      {
        OS.Activate();
        return !OS._isBit64;
      }
    }

    public static bool X64
    {
      get
      {
        OS.Activate();
        return OS._isBit64;
      }
    }
  }
}
