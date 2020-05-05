using ChartingObjects = OpenChart.Charting.Objects;
using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.Widgets
{
    public class TapNote : INoteFieldChartObject
    {
        ChartingObjects.TapNote note;
        Image widget;

        public ChartingObjects.BaseObject GetChartObject() => note;
        public Gtk.Widget GetWidget() => widget;

        public TapNote(ImageAsset noteImage, ChartingObjects.TapNote note)
        {
            if (note == null)
                throw new ArgumentNullException("Tap note object cannot be null.");

            widget = new Image(noteImage);
            this.note = note;
        }

        public int GetWidgetCenterOffset()
        {
            return widget.ImageAsset.Pixbuf.Height / 2;
        }
    }
}
