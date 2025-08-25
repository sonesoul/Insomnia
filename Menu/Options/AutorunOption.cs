using Insomnia.DirectMedia.Types;
using Insomnia.Menu.Renderers;
using Insomnia.Menu.Values;
using Insomnia.DirectMedia;
using System;

namespace Insomnia.Menu.Options
{
    public class AutorunOption : Option
    {
        private const string OptionName = "Autorun";
        private const string OptionDescription = "Start the program with system";
        private const string ValueDescription = "Select autorun state";

        public AutorunOption(Window window) : base(OptionName)
        {
            Description = OptionDescription;

            SwitchValue value = new(Autorun.Exists(), this);
            value.Renderer = new SwitchRenderer(value, window);
            value.Applied += () => SetValue(value.IsOn);
            value.Description = ValueDescription;

            Renderer = new OptionRenderer(this, window);
            Value = value;
        }

        private void SetValue(bool autoruns)
        {
            if (autoruns)
                Autorun.Add();
            else
                Autorun.Remove();
        }
    }
}