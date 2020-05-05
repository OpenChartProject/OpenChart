using Gdk;
using Gtk;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    public class NoteField : Layout
    {
        HBox keyContainer;
        BeatLines beatLines;
        List<Widget> widgetStack;

        public readonly RGBA BackgroundColor = new RGBA
        {
            Red = 0.1,
            Green = 0.1,
            Blue = 0.1,
            Alpha = 1.0,
        };

        int _keyWidth;
        public int KeyWidth
        {
            get => _keyWidth;
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Width must be greater than zero.");

                _keyWidth = value;

                NoteFieldData.NoteSkin.ScaleToNoteFieldKeyWidth(_keyWidth);
            }
        }

        public int NoteFieldWidth => KeyWidth * NoteFieldData.Chart.KeyCount.Value;
        public const int VerticalMargin = 100;

        public readonly NoteFieldData NoteFieldData;
        public readonly NoteFieldKey[] Keys;

        public NoteField(NoteFieldData noteFieldData, int keyWidth) : base(null, null)
        {
            if (noteFieldData == null)
                throw new ArgumentNullException("Note field data cannot be null.");

            NoteFieldData = noteFieldData;

            widgetStack = new List<Widget>();
            beatLines = new BeatLines(NoteFieldData);
            keyContainer = new HBox();
            Keys = new NoteFieldKey[NoteFieldData.Chart.KeyCount.Value];

            for (var i = 0; i < NoteFieldData.Chart.KeyCount.Value; i++)
            {
                Keys[i] = new NoteFieldKey(NoteFieldData, i, true);
                keyContainer.Add(Keys[i]);
            }

            keyContainer.MarginTop = VerticalMargin;
            keyContainer.MarginBottom = VerticalMargin;

            beatLines.MarginTop = VerticalMargin;
            beatLines.MarginBottom = VerticalMargin;

            Add(beatLines);
            Add(keyContainer);

            ScrollEvent += onScroll;

            SizeAllocated += (o, e) =>
            {
                beatLines.SetSizeRequest(NoteFieldWidth, e.Allocation.Height);
            };

            NoteFieldData.ChartEvents.ObjectAdded += (o, e) =>
            {
                Keys[e.Object.KeyIndex.Value].Add(e.Object);
            };
        }

        public new void Add(Widget widget)
        {
            widgetStack.Add(widget);
            base.Add(widget);
            scrollWidget(widget);
        }

        protected override bool OnDrawn(Cairo.Context cr)
        {
            Gdk.CairoHelper.SetSourceRgba(cr, BackgroundColor);
            cr.Paint();

            foreach (var widget in widgetStack)
            {
                PropagateDraw(widget, cr);
            }

            return true;
        }

        private void onScroll(object o, ScrollEventArgs e)
        {
            NoteFieldData.UpdateScroll(e.Event.Y, AllocatedHeight);

            foreach (var widget in widgetStack)
            {
                scrollWidget(widget);
            }
        }

        private void scrollWidget(Widget widget)
        {
            Move(widget, 0, 0 - NoteFieldData.ScrollTop.Position);
        }
    }
}
