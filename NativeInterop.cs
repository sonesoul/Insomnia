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

        [StructLayout(LayoutKind.Sequential)]
        struct INPUT
        {
            public uint type;
            public InputUnion U;
        }
        
        [StructLayout(LayoutKind.Explicit)]
        struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT mi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        public const int SW_HIDE = 0;
        public const int SW_SHOW = 5;
        public const int SW_RESTORE = 9;

        public const uint INPUT_MOUSE = 0;
        public const uint MOUSEEVENTF_MOVE = 0x0001;

        [DllImport("user32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);
        [DllImport("user32.dll")]
        private static unsafe extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        public static TimeSpan GetIdleTime()
        {
            LASTINPUTINFO info = new();
            info.cbSize = (uint)Marshal.SizeOf(info);
            GetLastInputInfo(ref info);

            uint idleTicks = ((uint)Environment.TickCount - info.dwTime);
            return TimeSpan.FromMilliseconds(idleTicks);
        }

        public static void MoveMouseBy(int deltaX, int deltaY)
        {
            INPUT input = new()
            {
                type = INPUT_MOUSE,
                U = new InputUnion
                {
                    mi = new MOUSEINPUT
                    {
                        dx = deltaX,
                        dy = deltaY,
                        mouseData = 0,
                        dwFlags = MOUSEEVENTF_MOVE,
                        time = 0,
                        dwExtraInfo = IntPtr.Zero
                    }
                }
            };

            //handling HResult in process
            uint HResult = SendInput(1, new INPUT[] { input }, Marshal.SizeOf<INPUT>());
        }
    }
}
