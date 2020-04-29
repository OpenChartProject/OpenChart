using Gtk;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.UI.Widgets
{
    public class NoteField : Layout
    {
        Cairo.Color backgroundColor = new Cairo.Color(0, 0, 0);
        Box keysContainer;

        public NoteFieldKey[] keys;
        public readonly KeyCount KeyCount;

        public NoteField(KeyCount keyCount) : base(null, null)
        {
            KeyCount = keyCount;
            keys = new NoteFieldKey[KeyCount.Value];
            keysContainer = new Box(Orientation.Horizontal, 0);

            for (var i = 0; i < KeyCount.Value; i++)
            {
                keys[i] = new NoteFieldKey();
                keysContainer.Add(keys[i]);
            }

            Add(keysContainer);
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
    }
}
