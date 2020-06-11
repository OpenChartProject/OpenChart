using OpenChart.UI.Assets;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Represents a tap note that is displayed on the note field.
    /// </summary>
    public class TapNote : INoteFieldObject
    {
        /// <summary>
        /// Gets the height of the tap note image.
        /// </summary>
        public int GetHeight() => ImageAsset.Pixbuf.Height;

        Charting.Objects.TapNote chartObject;

        /// <summary>
        /// Returns the tap note object from the chart.
        /// </summary>
        public Charting.Objects.BaseObject GetChartObject() => chartObject;

        /// <summary>
        /// The image this tap note is displaying.
        /// </summary>
        public ImageAsset ImageAsset { get; private set; }

        Image widget;

        /// <summary>
        /// Returns the widget for the tap note.
        /// </summary>
        public Gtk.Widget GetWidget() => widget;

        /// <summary>
        /// Creates a new TapNote instance.
        /// </summary>
        /// <param name="noteImage">The asset for the tap note.</param>
        /// <param name="chartObject">The note object from the chart.</param>
        public TapNote(ImageAsset noteImage, Charting.Objects.TapNote chartObject)
        {
            ImageAsset = noteImage;
            widget = new Image(ImageAsset);
            this.chartObject = chartObject;
        }
    }
}
