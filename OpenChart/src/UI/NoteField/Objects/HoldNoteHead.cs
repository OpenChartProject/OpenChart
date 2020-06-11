using OpenChart.UI.Assets;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Widget for the head of a hold note.
    /// </summary>
    public class HoldNoteHead : IWidget
    {
        /// <summary>
        /// The image asset this is displaying.
        /// </summary>
        public ImageAsset ImageAsset { get; private set; }

        Image widget;

        /// <summary>
        /// Returns the Gtk widget for the hold note head.
        /// </summary>
        public Gtk.Widget GetWidget() => widget;

        /// <summary>
        /// Creates a new HoldNoteHead instance.
        /// </summary>
        public HoldNoteHead(ImageAsset image)
        {
            ImageAsset = image;
            widget = new Image(ImageAsset);
        }
    }
}
