using Gtk;
using ChartingObjects = OpenChart.Charting.Objects;
using OpenChart.UI.Assets;
using System;

namespace OpenChart.UI.Widgets
{
    public class HoldNote : INoteFieldChartObject
    {
        HoldNoteBody holdBodyWidget;
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
            holdBodyWidget = new HoldNoteBody(holdBody);
            noteWidget = new Image(noteImage);
            this.note = note;

            var holdStartPos = noteField.GetYPosOfTime(note.Time.Value);
            var holdEndPos = noteField.GetYPosOfTime(note.Duration.Value);

            holdBodyWidget.SetSizeRequest(noteField.KeyWidth, holdEndPos - holdStartPos);

            containerWidget.Put(holdBodyWidget, 0, GetWidgetCenterOffset());
            containerWidget.Put(noteWidget, 0, 0);
        }

        public int GetWidgetCenterOffset()
        {
            return noteWidget.ImageAsset.Pixbuf.Height / 2;
        }
    }
}
