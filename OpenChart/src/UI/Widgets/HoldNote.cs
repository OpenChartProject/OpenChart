using Gtk;
using ChartingObjects = OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// Represents a hold note that is displayed on a note field.
    ///
    /// A hold note has an image for when the note starts, and a repeating image used
    /// for the body of the hold note.
    /// </summary>
    public class HoldNote : INoteFieldChartObject
    {
        HoldNoteBody holdBodyWidget;
        Image noteWidget;
        Fixed containerWidget;
        ChartingObjects.HoldNote note;
        readonly NoteFieldData noteFieldData;

        public Gtk.Widget GetWidget() => containerWidget;
        public ChartingObjects.BaseObject GetChartObject() => note;

        /// <summary>
        /// Creates a new HoldNote instance.
        /// </summary>
        /// <param name="noteFieldData">The note field data this will be displayed on.</param>
        /// <param name="holdImage">The asset for the hold note.</param>
        /// <param name="holdBodyImage">The asset for the hold note body.</param>
        /// <param name="note">The native note object.</param>
        public HoldNote(
            NoteFieldData noteFieldData,
            ImageAsset holdImage,
            ImageAsset holdBodyImage,
            ChartingObjects.HoldNote note
        )
        {
            if (noteFieldData == null)
                throw new ArgumentNullException("Note field data cannot be null.");
            else if (holdImage == null)
                throw new ArgumentNullException("Hold image cannot be null.");
            else if (holdBodyImage == null)
                throw new ArgumentNullException("Hold body image cannot be null.");
            else if (note == null)
                throw new ArgumentNullException("Chart object cannot be null.");

            this.note = note;
            this.noteFieldData = noteFieldData;

            // Create a container widget for the hold widgets.
            containerWidget = new Fixed();
            holdBodyWidget = new HoldNoteBody(holdBodyImage);
            noteWidget = new Image(holdImage);

            containerWidget.Put(holdBodyWidget, 0, GetWidgetCenterOffset());
            containerWidget.Put(noteWidget, 0, 0);

            Update();
        }

        public int GetWidgetCenterOffset()
        {
            return noteWidget.ImageAsset.Pixbuf.Height / 2;
        }

        /// <summary>
        /// Updates the size of the hold note body.
        /// </summary>
        public void Update()
        {
            var holdStartPos = noteFieldData.GetPosition(note.Beat);
            var holdEndPos = noteFieldData.GetPosition(new Beat(note.Beat.Value + note.Length.Value));

            holdBodyWidget.SetSizeRequest(noteFieldData.KeyWidth, holdEndPos - holdStartPos);
        }
    }
}
