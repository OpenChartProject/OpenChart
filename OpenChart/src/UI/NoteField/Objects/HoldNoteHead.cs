using OpenChart.UI.Assets;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Widget for the head of a hold note.
    /// </summary>
    public class HoldNoteHead : IWidget
    {
        Image widget;
        public Gtk.Widget GetWidget() => widget;

        /// <summary>
        /// Creates a new HoldNoteHead instance.
        /// </summary>
        public HoldNoteHead(ImageAsset image)
        {
            widget = new Image(image);
        }
    }
}
