using OpenChart.Charting.Properties;
using System;

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
        public HoldNote(KeyIndex key, Beat beat, BeatDuration length) : base(key, beat, length) { }

        public override bool Equals(object obj)
        {
            if (obj is HoldNote note)
                return (
                    KeyIndex.Equals(note.KeyIndex) &&
                    Beat.Equals(note.Beat) &&
                    Length.Equals(note.Length)
                );

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(KeyIndex, Beat, Length).GetHashCode();
        }
    }
}
