using OpenChart.UI.Assets;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Represents a tap note that is displayed on the note field.
    /// </summary>
    public class TapNote : INoteFieldObject
    {
        Charting.Objects.TapNote chartObject;
        public Charting.Objects.BaseObject GetChartObject() => chartObject;

        Image widget;
        public Gtk.Widget GetWidget() => widget;

        /// <summary>
        /// Creates a new TapNote instance.
        /// </summary>
        /// <param name="noteImage">The asset for the tap note.</param>
        /// <param name="chartObject">The note object from the chart.</param>
        public TapNote(ImageAsset noteImage, Charting.Objects.TapNote chartObject)
        {
            widget = new Image(noteImage);
            this.chartObject = chartObject;
        }
    }
}
