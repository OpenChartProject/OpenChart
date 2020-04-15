using System;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// The base class for a chart object which has a length or duration associated with it.
    /// </summary>
    public abstract class BaseLongObject : BaseObject
    {
        /// <summary>
        /// The exception thrown by `CanBeInserted()` if there is an object overlap.
        /// </summary>
        public ChartException OverlapException => new ChartException("This object overlaps with another object.");

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

        public BaseLongObject(int key, double beat, double length) : base(key, beat)
        {
            Length = length;
        }

        /// <summary>
        /// Checks if the object will overlap with an object that comes befores or after it.
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        public override void CanBeInserted(BaseObject prev, BaseObject next)
        {
            if (prev is BaseLongObject)
            {
                prev.CanBeInserted(null, this);
            }

            if (next != null && next.Beat <= (Beat + Length))
            {
                throw OverlapException;
            }
        }
    }
}
