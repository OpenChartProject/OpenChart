using OpenChart.Formats.StepMania.SM.Enums;
using System;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents a single beat row.
    /// </summary>
    public class BeatRow
    {
        /// <summary>
        /// The number of keys for the row.
        /// </summary>
        public readonly int KeyCount;

        /// <summary>
        /// The note types for the row.
        /// </summary>
        public readonly NoteType[] Notes;

        /// <summary>
        /// Creates a new BeatRow instance.
        /// </summary>
        public BeatRow(int keyCount)
        {
            KeyCount = keyCount;
            Notes = new NoteType[KeyCount];

            for (var i = 0; i < KeyCount; i++)
            {
                Notes[i] = NoteType.Empty;
            }
        }

        /// <summary>
        /// Sets a note for the given key.
        /// </summary>
        /// <param name="key">The key index.</param>
        /// <param name="noteType">The note type.</param>
        public void Set(int key, NoteType noteType)
        {
            if (key < 0 || key >= KeyCount)
                throw new IndexOutOfRangeException();

            Notes[key] = noteType;
        }
    }
}
