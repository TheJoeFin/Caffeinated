using System.Runtime.InteropServices;

namespace Caffeinated;

internal static class NativeMethods {
    [DllImport("kernel32.dll")]
    public static extern uint SetThreadExecutionState(uint esFlags);
    public const uint ES_CONTINUOUS = 0x80000000;
    public const uint ES_SYSTEM_REQUIRED = 0x00000001;
    public const uint ES_DISPLAY_REQUIRED = 0x00000002;

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
    public static extern int RegisterApplicationRestart(
        string? pwzCommandline,
        int dwFlags);

    public const int RESTART_NO_CRASH = 1;
    public const int RESTART_NO_HANG = 2;
    public const int RESTART_NO_PATCH = 4;
    public const int RESTART_NO_REBOOT = 8;
}
