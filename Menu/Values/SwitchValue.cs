using System;

namespace Insomnia.Menu.Values
{
    public class SwitchValue(bool value, Option option) : OptionValue(option)
    {
        public bool IsOn { get; private set; } = value;

        public event Action TurnedOn;
        public event Action TurnedOff;

        private bool _actualValue = value;

        public override void Up() => Switch();
        public override void Down() => Switch();

        private void Switch()
        {
            IsOn = !IsOn;

            if (IsOn)
                TurnedOn?.Invoke();
            else
                TurnedOff?.Invoke();
        }

        public override void Apply()
        {
            _actualValue = IsOn;
            base.Apply();
        }
        public override void Discard()
        {
            if (_actualValue != IsOn)
                Switch();

            base.Discard();
        }
    }
}