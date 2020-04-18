using OpenChart.Charting;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// The base class for a chart object that occurs on a specific key at a certain beat.
    /// </summary>
    public abstract class BaseObject
    {
        public Beat Beat;

        public KeyIndex Key;

        public BaseObject(KeyIndex key, Beat beat)
        {
            Beat = beat;
            Key = key;
        }

        /// <summary>
        /// Checks if the intended location for the object is valid or not. This method should
        /// throw a `ChartException` if the location is invalid (with the exception message
        /// being the reason why it's invalid).
        ///
        /// It's never valid to insert two objects that occur on the same beat and key.
        /// </summary>
        /// <param name="prev">The object immediately before this (or null).</param>
        /// <param name="next">The object immediately after this (or null).</param>
        public virtual void CanBeInserted(BaseObject prev, BaseObject next) { }
    }
}
