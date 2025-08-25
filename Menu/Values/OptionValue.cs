using Insomnia.Menu.Renderers;
using System;

namespace Insomnia.Menu.Values
{
    public abstract class OptionValue(Option option)
    {
        public Option Option { get; } = option;
        public ValueRenderer Renderer { get; set; }
        public string Description { get; set; } = string.Empty;

        public Action Applied;
        public event Action Discarded;

        public virtual void Apply() => Applied?.Invoke();
        public virtual void Discard() => Discarded?.Invoke();

        public abstract void Up();
        public abstract void Down();
    }
}