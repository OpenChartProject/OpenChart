using Gtk;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteFieldKey : Fixed
    {
        LinkedList<INoteFieldObject> objects;

        public int BeatSpacing { get; set; }

        public NoteFieldKey()
        {
            BeatSpacing = 50;
            objects = new LinkedList<INoteFieldObject>();
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

            if (objects.Count == 0)
            {
                objects.AddFirst(obj);
                return;
            }

            var cursor = objects.First;

            while (cursor != null)
            {
                var cursorObject = cursor.Value.GetChartObject();

                if (beat.Value == cursorObject.Beat.Value)
                    throw new ArgumentException("An object at the given beat already exists.");
                else if (beat.Value < cursorObject.Beat.Value)
                {
                    if (cursor.Previous == null || cursor.Previous.Value.GetChartObject().Beat.Value < beat.Value)
                    {
                        objects.AddBefore(cursor, obj);
                        return;
                    }
                }

                cursor = cursor.Next;
            }

            objects.AddLast(obj);
        }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            foreach (var obj in objects)
            {
                PropagateDraw(obj.GetWidget(), cr);
            }

            return true;
        }

        private void updateObject(INoteFieldObject obj)
        {
            Move(
                obj.GetWidget(),
                0,
                (int)(obj.GetChartObject().Beat.Value * BeatSpacing)
            );
        }
    }
}
