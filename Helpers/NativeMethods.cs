using System.Runtime.InteropServices;
using System.Drawing;

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

    [StructLayout(LayoutKind.Sequential)]
    public struct NOTIFYICONIDENTIFIER {
        public int cbSize;
        public nint hWnd;
        public uint uID;
        public System.Guid guidItem;
    }

    [DllImport("shell32.dll", SetLastError = true)]
    public static extern int Shell_NotifyIconGetRect(
        [In] ref NOTIFYICONIDENTIFIER identifier,
        [Out] out RECT iconLocation);

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public Rectangle ToRectangle() {
            return Rectangle.FromLTRB(left, top, right, bottom);
        }
    }
}
