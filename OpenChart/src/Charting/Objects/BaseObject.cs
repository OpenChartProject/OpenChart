using OpenChart.Charting.Properties;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// The base class for a chart object that occurs on a specific key at a certain beat.
    /// </summary>
    public abstract class BaseObject : IBeatObject, IKeyedObject, ITimedObject
    {
        public Beat Beat { get; private set; }
        public KeyIndex KeyIndex { get; private set; }
        public Time Time { get; set; }

        public BaseObject(KeyIndex key, Beat beat)
        {
            Beat = beat;
            KeyIndex = key;
        }

        /// <summary>
        /// This method is used by beat objects that are put into a list, sorted by beats.
        /// This is called right before insertion, with the previous and next objects in the list.
        /// These objects can be null.
        ///
        /// If there is a reason that this object cannot be inserted between prev and next,
        /// it should raise a `ChartException` with a reason why.
        /// </summary>
        public virtual void ValidatePlacement(IBeatObject prev, IBeatObject next) { }
    }
}
