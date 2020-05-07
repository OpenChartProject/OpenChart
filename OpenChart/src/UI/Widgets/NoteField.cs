using Gdk;
using Gtk;
using System;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// This class is simply a container that wires everything up for the different components
    /// of the note field to work. It's responsible for handling things like scrolling, and
    /// handling/orchestrating chart events.
    ///
    /// The note field components themselves (BeatLines, NoteFieldKey, etc.) are meant to be
    /// strictly presentational, meaning they don't really have any logic around knowing when
    /// to update themselves. Hence, this class -- which contains those components -- can listen
    /// to chart events and update the components accordingly.
    /// </summary>
    public class NoteField : Layout
    {
        /// <summary>
        /// The container for <see cref="Keys" />
        /// </summary>
        HBox keyContainer;
        List<Widget> widgetStack;

        /// <summary>
        /// The background color of the note field.
        ///
        /// TODO: This exists mainly for testing purposes. The note field should have a transparent
        /// background so we can show things like the audio wave form, chart bg image, etc.
        /// </summary>
        public readonly RGBA BackgroundColor = new RGBA
        {
            Red = 0.1,
            Green = 0.1,
            Blue = 0.1,
            Alpha = 1.0,
        };

        /// <summary>
        /// The width of the note field widget.
        /// </summary>
        public int NoteFieldWidth => NoteFieldData.KeyWidth * NoteFieldData.Chart.KeyCount.Value;

        /// <summary>
        /// The data/state for the note field.
        /// </summary>
        public readonly NoteFieldData NoteFieldData;

        /// <summary>
        /// A widget for displaying beat lines.
        /// </summary>
        public readonly BeatLines BeatLines;

        /// <summary>
        /// The widgets for displaying chart objects.
        /// </summary>
        public readonly NoteFieldKey[] Keys;

        /// <summary>
        /// Creates a new NoteField instance.
        /// </summary>
        /// <param name="noteFieldData">The data/state for the note field.</param>
        public NoteField(NoteFieldData noteFieldData) : base(null, null)
        {
            if (noteFieldData == null)
                throw new ArgumentNullException("Note field data cannot be null.");

            NoteFieldData = noteFieldData;

            widgetStack = new List<Widget>();
            BeatLines = new BeatLines(NoteFieldData);
            keyContainer = new HBox();
            Keys = new NoteFieldKey[NoteFieldData.Chart.KeyCount.Value];

            // Create the NoteFieldKey widgets and add them to the container.
            for (var i = 0; i < NoteFieldData.Chart.KeyCount.Value; i++)
            {
                Keys[i] = new NoteFieldKey(NoteFieldData, i);
                keyContainer.Add(Keys[i]);
            }

            // TODO: Size should be updated when the chart height changes.
            BeatLines.SetSizeRequest(NoteFieldWidth, NoteFieldData.ChartHeight);

            Add(BeatLines);
            Add(keyContainer);

            // Handle the user scrolling the note field.
            ScrollEvent += (o, e) =>
            {
                // "Scroll" (move) the widgets if the scroll position changed.
                if (NoteFieldData.OnScroll(e.Event.DeltaY, AllocatedHeight))
                    scrollAllWidgets();
            };

            // Handle the widget resizing.
            SizeAllocated += (o, e) =>
            {
                NoteFieldData.OnViewportResize(e.Allocation.Height);
            };

            // Handle a new object being added to the chart.
            NoteFieldData.ChartEvents.ObjectAdded += (o, e) => Keys[e.Object.KeyIndex.Value].Add(e.Object);
        }

        /// <summary>
        /// Adds the widget to the top of the widget stack.
        /// </summary>
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

            // Render each widget in the stack in the order they were added.
            foreach (var widget in widgetStack)
            {
                PropagateDraw(widget, cr);
            }

            return true;
        }

        private void scrollAllWidgets()
        {
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
