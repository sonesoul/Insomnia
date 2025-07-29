using System;

namespace Insomnia
{
    public class AwakeKeeper
    {
        public Point MousePosition { get; private set; } = new();
        public int OffsetPx { get; set; } = 10; 
        public TimeSpan IdleThreshold { get; set; } = TimeSpan.FromSeconds(1);
        public float MoveDelaySeconds { get; set; } = 1f;
        public bool Enabled { get; set; } = true;

        private bool _toggle = true;
        
        public void Update()
        {
            if (!Enabled)
                return;

            if (NativeInterop.GetIdleTime() >= IdleThreshold)
            {
                NativeInterop.MoveMouseBy((_toggle = !_toggle) ? -OffsetPx : OffsetPx, 0);
            }
        }
    }
}