using Gtk;
using OpenChart.UI.Assets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Widget for the hold note body. Renders a repeating image for the length of the hold note.
    /// </summary>
    public class HoldNoteBody : IWidget
    {
        /// <summary>
        /// The image pattern used to display the body.
        /// </summary>
        public ImagePattern BodyImage { get; private set; }

        DrawingArea drawingArea;
        public Widget GetWidget() => drawingArea;

        /// <summary>
        /// Creates a new HoldNoteBody instance.
        /// </summary>
        /// <param name="image">The image of the hold note body.</param>
        public HoldNoteBody(ImagePattern imagePattern)
        {
            BodyImage = imagePattern;

            drawingArea = new DrawingArea();
            drawingArea.Drawn += onDrawn;
        }

        private void onDrawn(object o, DrawnArgs e)
        {
            e.Cr.SetSource(BodyImage.Pattern);
            e.Cr.Rectangle(0, 0, drawingArea.AllocatedWidth, drawingArea.AllocatedHeight);
            e.Cr.Fill();
        }
    }
}
