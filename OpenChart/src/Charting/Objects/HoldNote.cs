using System;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// A hold note represents a tap note which must also be held for a certain duration.
    /// </summary>
    public class HoldNote : ChartObject
    {
        double _length;

        /// <summary>
        /// The duration (in beats) that the hold note lasts for.
        /// </summary>
        public double Length
        {
            get => _length;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Length must be greater than zero.");
                }

                _length = value;
            }
        }

        /// <summary>
        /// Creates a new HoldNote instance.
        /// </summary>
        /// <param name="key">The key index the note is on.</param>
        /// <param name="beat">The beat the hold note starts.</param>
        /// <param name="length">The length of the hold note (in beats).</param>
        public HoldNote(int key, double beat, double length) : base(key, beat)
        {
            Length = length;
        }

        public override void CanBeInserted(ChartObject prev, ChartObject next)
        {
            if (next != null && next.Beat <= (Beat + Length))
            {
                throw new ChartException("Hold note is too long and obstructs another object.");
            }
        }
    }
}