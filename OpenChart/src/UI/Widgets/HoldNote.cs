using Gtk;
using ChartingObjects = OpenChart.Charting.Objects;
using OpenChart.UI;
using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.Widgets
{
    public class HoldNote : INoteFieldChartObject
    {
        Image holdBodyWidget;
        Image noteWidget;
        Fixed containerWidget;
        ChartingObjects.HoldNote note;
        NoteField noteField;

        public Gtk.Widget GetWidget() => containerWidget;
        public ChartingObjects.BaseObject GetChartObject() => note;

        public HoldNote(NoteField noteField, ImageAsset noteImage, ImageAsset holdBody, ChartingObjects.HoldNote note)
        {
            if (note == null)
                throw new ArgumentNullException("Hold note object cannot be null.");

            this.noteField = noteField;

            containerWidget = new Fixed();
            holdBodyWidget = new Image(holdBody);
            noteWidget = new Image(noteImage);
            this.note = note;

            var beatToTime = noteField.Chart.BPMList.Time.BeatToTime(note.Beat.Value + note.Length.Value);
            holdBodyWidget.SetSizeRequest(0, noteField.GetYPosOfTime(beatToTime));

            containerWidget.Put(noteWidget, 0, 0);
            containerWidget.Put(holdBodyWidget, 0, GetWidgetCenterOffset());
        }

        public int GetWidgetCenterOffset()
        {
            return noteWidget.ImageAsset.Pixbuf.Height / 2;
        }
    }
}
