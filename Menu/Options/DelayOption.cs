using Insomnia.DirectMedia;
using Insomnia.Menu.Renderers;
using Insomnia.Menu.Values;
using System;

namespace Insomnia.Menu.Options
{
    public class DelayOption : Option
    {
        private static AwakeKeeper AwakeKeeper => Program.AwakeKeeper;

        private const string OptionName = "Delay";
        private const string OptionDescription = "Delay between moves";
        private const string ValueDescription = "Select delay";

        public DelayOption(Window window) : base(OptionName)
        {
            Description = OptionDescription;

            var thresholdSpan = AwakeKeeper.IdleThreshold;

            TimeMetric metric = TimeMetric.Seconds;
            int timeValue = thresholdSpan.Seconds;

            if (thresholdSpan.Minutes >= 1)
            {
                timeValue = thresholdSpan.Minutes;
                metric = TimeMetric.Minutes;
            }
            else if (thresholdSpan.Hours >= 1)
            {
                timeValue = thresholdSpan.Hours;
                metric = TimeMetric.Hours;
            }

            TimeRollValue value = new(metric, timeValue, this);
            value.Renderer = new TimeRollRenderer(value, window);
            value.Applied += () => SetDelay(value.Metric, value.Value);
            value.Description = ValueDescription;

            Renderer = new OptionRenderer(this, window);
            Value = value;
        }

        private void SetDelay(TimeMetric metric, int value)
        {
            TimeSpan time;

            if (metric == TimeMetric.Minutes)
                time = TimeSpan.FromMinutes(value);
            else if (metric == TimeMetric.Hours)
                time = TimeSpan.FromHours(value);
            else 
                time = TimeSpan.FromSeconds(value);

            AwakeKeeper.IdleThreshold = time; 
        }
    }
}