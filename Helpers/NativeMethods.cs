using System.Runtime.InteropServices;

namespace Caffeinated;

internal static partial class NativeMethods {
    [LibraryImport("kernel32.dll")]
    public static partial uint SetThreadExecutionState(uint esFlags);
    public const uint ES_CONTINUOUS = 0x80000000;
    public const uint ES_SYSTEM_REQUIRED = 0x00000001;
    public const uint ES_DISPLAY_REQUIRED = 0x00000002;
}
