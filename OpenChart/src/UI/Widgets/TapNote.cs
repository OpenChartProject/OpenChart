using ChartingObjects = OpenChart.Charting.Objects;
using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.Widgets
{
    public class TapNote : IChartObject
    {
        public readonly Image Widget;
        public Gtk.Widget GetWidget() => Widget;

        public readonly ChartingObjects.TapNote Note;
        public ChartingObjects.BaseObject GetChartObject() => Note;

        public TapNote(ImageAsset imageAsset, ChartingObjects.TapNote note)
        {
            if (note == null)
                throw new ArgumentNullException("Tap note object cannot be null.");

            Widget = new Image(imageAsset);
            Note = note;
        }
    }
}
