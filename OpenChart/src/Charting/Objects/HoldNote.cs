namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// A hold note represents a tap note which must also be held for a certain duration.
    /// </summary>
    public class HoldNote : BaseLongObject
    {
        /// <summary>
        /// Creates a new HoldNote instance.
        /// </summary>
        /// <param name="key">The key index the note is on.</param>
        /// <param name="beat">The beat the hold note starts.</param>
        /// <param name="length">The length of the hold note (in beats).</param>
        public HoldNote(int key, double beat, double length) : base(key, beat, length) { }
    }
}
