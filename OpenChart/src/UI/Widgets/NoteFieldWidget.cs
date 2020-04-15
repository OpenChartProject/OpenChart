using Gtk;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteFieldWidget : Layout
    {
        public int KeyCount { get; private set; }

        public LinkedList<BaseNoteFieldWidget>[] NoteFieldObjects { get; private set; }

        public NoteFieldWidget(int keyCount) : base(null, null)
        {
            KeyCount = keyCount;
            NoteFieldObjects = new LinkedList<BaseNoteFieldWidget>[] { };

            for (var i = 0; i < KeyCount; i++)
            {
                NoteFieldObjects[i] = new LinkedList<BaseNoteFieldWidget>();
            }
        }

        public LinkedListNode<BaseNoteFieldWidget> Add(BaseNoteFieldWidget widget)
        {
            if (widget.Key >= KeyCount)
            {
                throw new ArgumentOutOfRangeException(
                    "Widget key index is larger than what the note field supports."
                );
            }

            return null;
        }
    }
}
