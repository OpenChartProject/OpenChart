using Gtk;
using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteField : Fixed
    {
        HBox keyContainer;
        BeatLines beatLines;
        List<Widget> widgetStack;

        public NoteFieldKey[] keys;
        public readonly KeyCount KeyCount;

        public NoteField(KeyCount keyCount) : base()
        {
            KeyCount = keyCount;

            widgetStack = new List<Widget>();
            beatLines = new BeatLines();
            keyContainer = new HBox();
            keys = new NoteFieldKey[KeyCount.Value];

            for (var i = 0; i < KeyCount.Value; i++)
            {
                keys[i] = new NoteFieldKey();
                keyContainer.Add(keys[i]);
            }

            Add(beatLines);
            Add(keyContainer);
        }

        public void Add(INoteFieldObject obj)
        {
            var keyIndex = obj.GetChartObject().KeyIndex.Value;

            if (keyIndex >= KeyCount.Value)
                throw new ArgumentOutOfRangeException(
                    "The chart object's key index is out of range for the notefield's key count."
                );

            keys[keyIndex].Add(obj);
        }

        public new void Add(Widget widget)
        {
            base.Add(widget);
            widgetStack.Add(widget);
        }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            foreach (var widget in widgetStack)
            {
                PropagateDraw(widget, cr);
            }

            return true;
        }
    }
}
