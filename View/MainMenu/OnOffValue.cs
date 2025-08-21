using System;

namespace Insomnia.View.MainMenu
{
    public class OnOffValue(Option option, bool value) : OptionValue(option)
    {
        public bool IsOn { get; private set; } = value;

        public event Action TurnedOff;
        public event Action TurnedOn;

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