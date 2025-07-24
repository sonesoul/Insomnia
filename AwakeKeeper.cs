using System;
using System.Drawing.Design;
using System.Numerics;
using WindowsInput;

namespace Insomnia
{
    public class AwakeKeeper() : MouseSimulator(new InputSimulator())
    {
        public Point MousePosition { get; private set; } = new();
        public int OffsetPx { get; set; } = 10; 
        public TimeSpan IdleThreshold { get; set; } = TimeSpan.FromSeconds(1);
        public float MoveDelaySeconds { get; set; } = 1f;

        private bool _toggle = true;
        private float _activityElapsed = 0;
        private float _moveElapsed = 0;

        public void Update()
        {
            if (NativeInterop.GetIdleTime() >= IdleThreshold)
            {
                MoveMouseBy((_toggle = !_toggle) ? -OffsetPx : OffsetPx, 0);
            }
        }
    }
}