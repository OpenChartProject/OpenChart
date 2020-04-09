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

        /// <summary>
        /// Creates a new BPM instance.
        /// </summary>
        /// <param name="value">The beats per minute.</param>
        /// <param name="beat">The beat this BPM change occurs on.</param>
        public BPM(double value, double beat) : base(beat)
        {
            Value = value;
        }

        /// <summary>
        /// Checks if both BPM objects are equal.
        /// </summary>
        public override bool Equals(object obj)
        {
            var bpm = obj as BPM;

            if (bpm == null)
            {
                return false;
            }

            return Beat == bpm.Beat && Value == bpm.Value;
        }

        /// <summary>
        /// Returns the object's hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return Tuple.Create(Beat, Value).GetHashCode();
        }
    }
}