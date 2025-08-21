using System;

namespace Insomnia.View.MainMenu
{
    public abstract class OptionValue(Option option)
    {
        public Option Option { get; } = option;
        public ValueRenderer Renderer { get; set; }

        public void RetakeUpDown(out Action up, out Action down)
        {
            up = Up;
            down = Down;
        }

        public abstract void Up();
        public abstract void Down();
    }
}