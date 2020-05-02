using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// A tap note represents a note that is it with a single keypress.
    /// </summary>
    public class TapNote : BaseObject
    {
        /// <summary>
        /// Creates a new TapNote instance.
        /// </summary>
        /// <param name="key">The key index the note is on.</param>
        /// <param name="beat">The beat the tap note occurs.</param>
        public TapNote(KeyIndex key, Beat beat) : base(key, beat) { }

        public override bool Equals(object obj)
        {
            if (obj is TapNote note)
                return KeyIndex.Equals(note.KeyIndex) && Beat.Equals(note.Beat);

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(KeyIndex, Beat).GetHashCode();
        }
    }
}
