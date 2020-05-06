using Gtk;
using ChartingObjects = OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
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
        readonly NoteFieldData noteFieldData;

        public Gtk.Widget GetWidget() => containerWidget;
        public ChartingObjects.BaseObject GetChartObject() => note;

        public HoldNote(
            NoteFieldData noteFieldData,
            ImageAsset noteImage,
            ImageAsset holdBody,
            ChartingObjects.HoldNote note
        )
        {
            if (note == null)
                throw new ArgumentNullException("Hold note object cannot be null.");

            this.noteFieldData = noteFieldData;

            containerWidget = new Fixed();
            holdBodyWidget = new HoldNoteBody(holdBody);
            noteWidget = new Image(noteImage);
            this.note = note;

            var holdStartPos = noteFieldData.GetPosition(note.Beat);
            var holdEndPos = noteFieldData.GetPosition(new Beat(note.Beat.Value + note.Length.Value));

            holdBodyWidget.SetSizeRequest(noteFieldData.KeyWidth, holdEndPos - holdStartPos);

            containerWidget.Put(holdBodyWidget, 0, GetWidgetCenterOffset());
            containerWidget.Put(noteWidget, 0, 0);
        }

        public int GetWidgetCenterOffset()
        {
            return noteWidget.ImageAsset.Pixbuf.Height / 2;
        }
    }
}
