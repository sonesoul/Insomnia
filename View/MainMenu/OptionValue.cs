using System;

namespace Insomnia.View.MainMenu
{
    public abstract class OptionValue(Option option)
    {
        public Option Option { get; } = option;
        public ValueRenderer Renderer { get; set; }

        public event Action Applied;
        public event Action Discarded;

        public virtual void Apply() => Applied?.Invoke();
        public virtual void Discard() => Discarded?.Invoke();

        public abstract void Up();
        public abstract void Down();
    }
}