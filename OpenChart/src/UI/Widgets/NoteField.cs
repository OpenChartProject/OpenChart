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

        public int NoteFieldWidth => NoteFieldData.KeyWidth * NoteFieldData.Chart.KeyCount.Value;

        public readonly NoteFieldData NoteFieldData;
        public readonly NoteFieldKey[] Keys;

        public NoteField(NoteFieldData noteFieldData) : base(null, null)
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
                Keys[i] = new NoteFieldKey(NoteFieldData, i);
                keyContainer.Add(Keys[i]);
            }

            beatLines.SetSizeRequest(NoteFieldWidth, NoteFieldData.ChartHeight);

            Add(beatLines);
            Add(keyContainer);

            ScrollEvent += onScroll;
            SizeAllocated += (o, e) =>
            {
                NoteFieldData.UpdateScroll(0, e.Allocation.Height);
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
            if (!NoteFieldData.UpdateScroll(e.Event.DeltaY, AllocatedHeight))
                return;

            foreach (var widget in widgetStack)
            {
                scrollWidget(widget);
            }
        }

        private void scrollWidget(Widget widget)
        {
            Move(widget, 0, 0 - NoteFieldData.ScrollTop.PositionWithOffset);
        }
    }
}
