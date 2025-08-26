using System;

namespace Insomnia
{
    public class AwakeKeeper
    {
        public Point MousePosition { get; private set; } = new();
        public int OffsetPx { get; set; } = 10; 
        public TimeSpan IdleThreshold { get => _idleThreshold; set => SetThreshold(value); } 
        public bool IsActive { get => _isActive; set => SetActive(value); } 

        public event Action<bool> ActiveStateChanged;

        public event Action<TimeSpan> IdleThresholdChanged;

        private bool _toggle = true;
        private bool _isActive = true;
        private TimeSpan _idleThreshold = TimeSpan.FromSeconds(1);

        public void Update()
        {
            if (!IsActive)
                return;

            if (NativeInterop.GetIdleTime() >= IdleThreshold)
            {
                NativeInterop.MoveMouseBy((_toggle = !_toggle) ? -OffsetPx : OffsetPx, 0);
            }
        }

        private void SetActive(bool isActive)
        {
            _isActive = isActive;
            ActiveStateChanged?.Invoke(isActive);
        }
        private void SetThreshold(TimeSpan idleThreshold)
        {
            _idleThreshold = idleThreshold;
            IdleThresholdChanged?.Invoke(idleThreshold);
        }

    }
}