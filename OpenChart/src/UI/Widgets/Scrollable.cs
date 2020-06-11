using Gtk;
using System;

namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// A container which wraps a widget and provides horizontal and vertical scrolling.
    /// </summary>
    public class Scrollable : IWidget
    {
        Layout layout;
        public Widget GetWidget() => layout;

        /// <summary>
        /// The widget being wrapped.
        /// </summary>
        public Widget WrappedWidget { get; private set; }

        /// <summary>
        /// Inverts vertical scrolling when true.
        /// </summary>
        public bool InvertY { get; set; }

        /// <summary>
        /// The distance, in pixels, that the container should scroll on the X-axis each scroll tick.
        /// </summary>
        public int HorizontalScrollSpeed { get; set; }

        /// <summary>
        /// The distance, in pixels, that the container should scroll on the Y-axis each scroll tick.
        /// </summary>
        public int VerticalScrollSpeed { get; set; }

        /// <summary>
        /// The X position of the wrapped widget.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// The Y position of the wrapped widget.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Creates a new Scrollable instance.
        /// </summary>
        /// <param name="widget">The widget to wrap.</param>
        /// <param name="xSpeed">The horizontal scroll speed, in pixels.</param>
        /// <param name="ySpeed">The vertical scroll speed, in pixels.</param>
        /// <param name="invertY">Inverts vertical scrolling when true.</param>
        public Scrollable(Widget widget, int xSpeed, int ySpeed, bool invertY = true)
        {
            InvertY = invertY;
            HorizontalScrollSpeed = xSpeed;
            VerticalScrollSpeed = ySpeed;
            WrappedWidget = widget;

            layout = new Layout(null, null);
            layout.Add(WrappedWidget);
            layout.ScrollEvent += onScroll;
        }

        /// <summary>
        /// Moves the wrapped widget to an absolute position.
        /// </summary>
        public void Move(int x, int y)
        {
            X = x;
            Y = y;

            layout.Move(WrappedWidget, X, Y);
        }

        /// <summary>
        /// Moves the wrapped widget relative to its current position.
        /// </summary>
        public void Translate(int x, int y)
        {
            Move(X + x, Y + y);
        }

        private void onScroll(object o, ScrollEventArgs e)
        {
            var invert = InvertY ? -1 : 1;

            Translate(
                (int)Math.Round(e.Event.DeltaX * HorizontalScrollSpeed),
                (int)Math.Round(e.Event.DeltaY * VerticalScrollSpeed) * invert
            );
        }
    }
}
