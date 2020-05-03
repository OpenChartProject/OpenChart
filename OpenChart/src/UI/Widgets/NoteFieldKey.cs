using Gtk;
using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteFieldKey : Fixed, IHasNoteField
    {
        Dictionary<Beat, INoteFieldChartObject> objects;

        public readonly KeyModeSkin NoteSkin;
        public readonly KeyIndex KeyIndex;
        public NoteField NoteField { get; set; }

        public NoteFieldKey(KeyIndex keyIndex, KeyModeSkin noteSkin)
        {
            KeyIndex = keyIndex;
            NoteSkin = noteSkin;

            objects = new Dictionary<Beat, INoteFieldChartObject>();
        }

        public void Add(NativeObjects.BaseObject chartObject)
        {
            var noteFieldObject = UIUtils.ChartObjectToWidget(chartObject, NoteSkin.Keys[KeyIndex.Value]);

            Add(noteFieldObject.GetWidget());
            objects[noteFieldObject.GetChartObject().Beat] = noteFieldObject;
            updateObject(noteFieldObject);
        }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            foreach (var obj in objects.Values)
            {
                PropagateDraw(obj.GetWidget(), cr);
            }

            return true;
        }

        private void updateObject(INoteFieldChartObject obj)
        {
            Move(
                obj.GetWidget(),
                0,
                (int)(obj.GetChartObject().Time.Value * NoteField.TimeSpacing)
            );
        }
    }
}
