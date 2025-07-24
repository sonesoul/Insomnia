using System;

namespace Insomnia
{
    public class AwakeKeeper
    {
        public Point MousePosition { get; private set; } = new();
        public int OffsetPx { get; set; } = 10; 
        public TimeSpan IdleThreshold { get; set; } = TimeSpan.FromSeconds(1);
        public float MoveDelaySeconds { get; set; } = 1f;

        private bool _toggle = true;
        
        public void Update()
        {
            if (NativeInterop.GetIdleTime() >= IdleThreshold)
            {
                NativeInterop.MoveMouseBy((_toggle = !_toggle) ? -OffsetPx : OffsetPx, 0);
            }
        }
    }
}