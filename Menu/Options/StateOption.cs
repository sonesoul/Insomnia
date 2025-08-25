using Insomnia.Menu.Values;
using Insomnia.Menu.Renderers;
using Insomnia.DirectMedia;

namespace Insomnia.Menu.Options
{
    public class StateOption : Option
    {
        private static AwakeKeeper AwakeKeeper => Program.AwakeKeeper;

        private const string OptionName = "State";
        private const string OptionDescription = "Mouse moving state";
        private const string ValueDescription = "Select state";

        public StateOption(Window window) : base(OptionName)
        {
            Description = OptionDescription;

            SwitchValue value = new(AwakeKeeper.IsActive, this);
            value.Renderer = new SwitchRenderer(value, window);
            value.Applied += () => SetActive(value.IsOn);
            value.Description = ValueDescription;

            Renderer = new OptionRenderer(this, window);
            Value = value;
        }

        private void SetActive(bool value)
        {
            AwakeKeeper.IsActive = value;
        }
    }
}