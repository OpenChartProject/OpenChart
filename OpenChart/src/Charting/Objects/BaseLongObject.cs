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

        public BeatDuration Length;

        public BaseLongObject(Key key, Beat beat, BeatDuration length) : base(key, beat)
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