using System;

namespace Insomnia
{
    public class AwakeKeeper
    {
        public Point MousePosition { get; private set; } = new();
        public int OffsetPx { get; set; } = 10; 
        public TimeSpan IdleThreshold { get; set; } = TimeSpan.FromSeconds(1);
        public float MoveDelaySeconds { get; set; } = 1f;
        public bool IsActive { get => _isActive; set => SetActive(value); } 

        public event Action<bool> ActiveStateChanged; 
       
        private bool _toggle = true;
        private bool _isActive = true;

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
    }
}