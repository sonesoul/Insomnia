using System;
using System.Collections.Generic;

namespace Insomnia.View.MainMenu
{
    public enum TimeMetric : byte
    {
        Seconds = 0, 
        Minutes = 1,
        Hours = 2,
    }

    public class TimeRollValue(Option option, TimeMetric metric, int value) : OptionValue(option)
    {
        public TimeMetric Metric => (TimeMetric)_metricIndex;
        public int Value { get; private set; } = value;

        public event Action<TimeMetric> MetricChanged;
        public event Action<int> ValueChanged;

        private int _metricIndex = (int)metric;
        private int _actualValue = value;

        private static Dictionary<TimeMetric, (byte min, byte max)> MetricBounds { get; } = new()
        {
            [TimeMetric.Seconds] = (1, 59),
            [TimeMetric.Minutes] = (1, 59),
            [TimeMetric.Hours] = (1, 23),
        };

        public override void Apply()
        {
            _actualValue = Value;
            base.Apply();
        }

        public override void Discard()
        {
            SetValue(_actualValue);
            base.Discard();
        }

        public override void Down() => SetValue(Value - 1);
        public override void Up() => SetValue(Value + 1);

        private void SetValue(int newValue)
        {
            (byte min, byte max) = MetricBounds[Metric];

            if (newValue > max)
            {
                _metricIndex = (_metricIndex + 1) % MetricBounds.Count;
                newValue = MetricBounds[Metric].min;

                MetricChanged?.Invoke(Metric);
            }
            else if (newValue < min)
            {
                _metricIndex -= 1;

                if (_metricIndex < 0)
                    _metricIndex = MetricBounds.Count - 1;

                newValue = MetricBounds[Metric].max;
                MetricChanged?.Invoke(Metric);
            }
            
            Value = newValue;

            ValueChanged?.Invoke(newValue);
        }
    }
}