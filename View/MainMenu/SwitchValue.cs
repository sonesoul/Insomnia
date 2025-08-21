using System;

namespace Insomnia.View.MainMenu
{
    public class SwitchValue(Option option, bool value) : OptionValue(option)
    {
        public bool IsOn { get; private set; } = value;

        public event Action TurnedOn;
        public event Action TurnedOff;

        public override void Up() => Toggle();
        public override void Down() => Toggle();

        private void Toggle()
        {
            IsOn = !IsOn;

            if (IsOn)
                TurnedOn?.Invoke();
            else
                TurnedOff?.Invoke();
        }
    }
}