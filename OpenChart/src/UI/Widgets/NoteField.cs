using Gtk;
using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteField : Layout
    {
        Cairo.Color backgroundColor = new Cairo.Color(0, 0, 0);

        LinkedList<INoteFieldObject>[] noteFieldObjects;

        public int BeatSpacing { get; set; }
        public int ColumnSpacing { get; set; }
        public readonly KeyCount KeyCount;

        public NoteField(KeyCount keyCount) : base(null, null)
        {
            BeatSpacing = 100;
            ColumnSpacing = 128;
            KeyCount = keyCount;
            noteFieldObjects = new LinkedList<INoteFieldObject>[KeyCount.Value];

            for (var i = 0; i < KeyCount.Value; i++)
            {
                noteFieldObjects[i] = new LinkedList<INoteFieldObject>();
            }
        }

        public void Add(INoteFieldObject obj)
        {
            addNoteFieldObject(obj);
            Add(obj.GetWidget());
            updateObject(obj);
        }

        private void addNoteFieldObject(INoteFieldObject obj)
        {
            var beat = obj.GetChartObject().Beat;
            var keyIndex = obj.GetChartObject().KeyIndex;

            if (keyIndex.Value >= KeyCount.Value)
                throw new ArgumentOutOfRangeException(
                    "Chart object cannot be added to note field because its key index " +
                    "is greater than the notefield's key count."
                );

            var list = noteFieldObjects[keyIndex.Value];

            if (list.Count == 0)
            {
                list.AddFirst(obj);
                return;
            }

            var cursor = list.First;

            while (cursor != null)
            {
                var cursorObject = cursor.Value.GetChartObject();

                if (beat.Value == cursorObject.Beat.Value)
                    throw new ArgumentException("An object at the given beat already exists.");
                else if (beat.Value < cursorObject.Beat.Value)
                {
                    if (cursor.Previous == null || cursor.Previous.Value.GetChartObject().Beat.Value < beat.Value)
                    {
                        list.AddBefore(cursor, obj);
                        return;
                    }
                }

                cursor = cursor.Next;
            }

            list.AddLast(obj);
        }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            cr.SetSourceColor(backgroundColor);
            cr.Paint();

            foreach (var keyObjects in noteFieldObjects)
            {
                foreach (var obj in keyObjects)
                {
                    PropagateDraw(obj.GetWidget(), cr);
                }
            }

            return true;
        }

        private void updateObject(INoteFieldObject obj)
        {
            Move(
                obj.GetWidget(),
                obj.GetChartObject().KeyIndex.Value * ColumnSpacing,
                (int)(obj.GetChartObject().Beat.Value * BeatSpacing)
            );
        }
    }
}
