using System;

namespace Insomnia.View.MainMenu
{
    public enum OptionState
    {
        Avaliable,
        Selected,
        Entered,
    }

    public class Option(Vector2 position)
    {
        public Vector2 Position { get; set; } = position;
        public OptionValue Value { get; set; }

        public OptionRenderer Renderer { get; set; }

        public bool IsActive { get => _isActive; set => SetIsActive(value); }
        public OptionState State { get => _state; set => SetState(value); }

        public event Action Activated;
        public event Action Deactivated;

        public event Action<OptionState> StateChanged;

        private bool _isActive = false;
        private OptionState _state = OptionState.Avaliable;

        public void Enter() => SetState(OptionState.Entered);
        public void Select() => SetState(OptionState.Selected);
        public void Reset() => SetState(OptionState.Avaliable);
        
        private void SetState(OptionState state)
        {
            if (!IsActive)
                return;

            _state = state;
            StateChanged?.Invoke(state);
        }
        private void SetIsActive(bool value)
        {
            if (Value == null && value == true)
                return;

            _isActive = value;

            if (_isActive)
            {
                Activated?.Invoke();
            }
            else
            {
                Deactivated?.Invoke();
            }
        }
    }
}