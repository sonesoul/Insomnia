using System;
using System.Collections.Generic;

namespace Insomnia.Menu.Values
{
    public enum TimeMetric : byte
    {
        Seconds = 0, 
        Minutes = 1,
        Hours = 2,
    }

    public class TimeRollValue(TimeMetric metric, int value, Option option) : OptionValue(option)
    {
        public TimeMetric Metric => (TimeMetric)_metricIndex;
        public int Value { get; private set; } = value;

        public event Action<TimeMetric> MetricChanged;
        public event Action<int> ValueChanged;

        private int _metricIndex = (int)metric;

        private int _actualValue = value;
        private int _actualIndex = (int)metric;

        private static Dictionary<TimeMetric, (byte min, byte max)> MetricBounds { get; } = new()
        {
            [TimeMetric.Seconds] = (1, 59),
            [TimeMetric.Minutes] = (1, 59),
            [TimeMetric.Hours] = (1, 23),
        };

        public override void Apply()
        {
            _actualValue = Value;
            _actualIndex = _metricIndex;
            base.Apply();
        }

        public override void Discard()
        {
            SetTime(_actualValue, _actualIndex);
            base.Discard();
        }

        public override void Down() => SetValue(Value - 1);
        public override void Up() => SetValue(Value + 1);

        private void SetValue(int value)
        {
            (byte min, byte max) = MetricBounds[Metric];

            int index = _metricIndex;
            int metricsCount = MetricBounds.Count;

            if (value > max)
            {
                index = (index + 1) % metricsCount;
                value = MetricBounds[(TimeMetric)index].min;
            }
            else if (value < min)
            {
                index = (index - 1 + metricsCount) % metricsCount;

                value = MetricBounds[(TimeMetric)index].max;
            }

            SetTime(value, index);
        }

        private void SetTime(int newValue, int metricIndex)
        {
            if (_metricIndex != metricIndex)
            {
                _metricIndex = metricIndex;
                MetricChanged?.Invoke(Metric);    
            }

            if (Value != newValue)
            {
                Value = newValue;
                ValueChanged?.Invoke(newValue);
            }
        }
    }
}