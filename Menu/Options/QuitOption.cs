using Insomnia.DirectMedia;
using Insomnia.Menu.Renderers;
using Insomnia.Menu.Values;

namespace Insomnia.Menu.Options
{
    public class QuitOption : Option
    {
        private const string OptionName = "Quit";
        private const string OptionDescription = "Quit the program";
        private const string ValueDescription = "Are you sure?";

        public QuitOption(Window window) : base(OptionName)
        {
            Description = OptionDescription;

            SwitchValue value = new(false, this);
            value.Renderer = new SwitchRenderer("Yes", "No", value, window);
            value.Applied += () => Quit(value.IsOn);
            value.Description = ValueDescription;

            Renderer = new OptionRenderer(this, window);
            Value = value;
        }

        private void Quit(bool value)
        {
            if (value)
            {
                Program.Quit();
            }
        }
    }
}
