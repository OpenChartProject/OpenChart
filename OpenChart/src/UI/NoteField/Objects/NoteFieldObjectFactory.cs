using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// A factory class which creates note field objects.
    /// </summary>
    public class NoteFieldObjectFactory
    {
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        /// <summary>
        /// Creates a new NoteFieldObjectFactory instance.
        /// </summary>
        public NoteFieldObjectFactory(NoteFieldSettings noteFieldSettings)
        {
            NoteFieldSettings = noteFieldSettings;
        }

        public INoteFieldObject Create(Charting.Objects.BaseObject chartObject)
        {
            INoteFieldObject obj;
            var keySkin = NoteFieldSettings.NoteSkin.Keys[chartObject.KeyIndex.Value];

            if (chartObject is Charting.Objects.TapNote tapNote)
                obj = new TapNote(keySkin.TapNote, tapNote);
            else if (chartObject is Charting.Objects.HoldNote holdNote)
                // TODO: Cache the hold note body on the note skin
                obj = new HoldNote(keySkin.HoldNote, new ImagePattern(keySkin.HoldNoteBody), holdNote);
            else
                throw new Exception("Unknown chart object type.");

            return obj;
        }
    }
}
