using System;
using System.Runtime.InteropServices;

namespace Insomnia
{
    internal static class NativeInterop
    {
        [StructLayout(LayoutKind.Sequential)]
        struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static TimeSpan GetIdleTime()
        {
            LASTINPUTINFO info = new();
            info.cbSize = (uint)Marshal.SizeOf(info);
            GetLastInputInfo(ref info);

            uint idleTicks = ((uint)Environment.TickCount - info.dwTime);
            return TimeSpan.FromMilliseconds(idleTicks);
        }
        
    }
}
