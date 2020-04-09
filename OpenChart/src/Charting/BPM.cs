using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a BPM (beats per minute) change in a chart.
    /// </summary>
    public class BPM : Timing
    {
        double _value;

        /// <summary>
        /// Beats per minute. Most songs are somewhere in the range of 120 to 200.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("BPM must be greater than zero.");
                }

                _value = value;
            }
        }

        public BPM(double value, double beat) : base(beat)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var bpm = obj as BPM;

            if (bpm == null)
            {
                return false;
            }

            return Beat == bpm.Beat && Value == bpm.Value;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Beat, Value).GetHashCode();
        }
    }
}