using System;

namespace OpenChart.Chart
{
    /// <summary>
    /// Represents a BPM (beats per minute) change in a chart.
    /// </summary>
    public class BPM : Timing
    {
        float _value;

        /// <summary>
        /// Beats per minute. Most songs are somewhere in the range of 120 to 200.
        /// </summary>
        public float Value
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

        public BPM(float value, float beat) : base(beat)
        {
            Value = value;
        }
    }
}