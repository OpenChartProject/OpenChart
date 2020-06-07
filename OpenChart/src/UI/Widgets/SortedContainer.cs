using Gtk;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// A container that sorts its children and then draws them in the order they are sorted.
    /// </summary>
    public class SortedContainer<T> : Fixed
    {
        SortedList<T, Widget> sortedChildren;

        /// <summary>
        /// Creates a new SortedContainer instance.
        /// </summary>
        public SortedContainer()
        {
            sortedChildren = new SortedList<T, Widget>();
        }

        /// <summary>
        /// Adds a widget to the container.
        /// </summary>
        /// <param name="key">The child's sort value.</param>
        /// <param name="widget">The child's widget.</param>
        public void Add(T key, Widget widget)
        {
            Put(key, widget, 0, 0);
        }

        /// <summary>
        /// Adds a widget to the container at the given position.
        /// </summary>
        /// <param name="key">The child's sort value.</param>
        /// <param name="widget">The child's widget.</param>
        /// <param name="x">X position.</param>
        /// <param name="y">Y position.</param>
        public void Put(T key, Widget widget, int x, int y)
        {
            Put(widget, x, y);
            sortedChildren.Add(key, widget);
        }

        /// <summary>
        /// Removes a widget from the container.
        /// </summary>
        /// <param name="widget">The widget to remove.</param>
        public void Remove(T key)
        {
            var widget = sortedChildren[key];

            sortedChildren.Remove(key);
            base.Remove(widget);
        }

        /// <summary>
        /// Draws the children in the order that they are sorted.
        /// </summary>
        public new bool Draw(Cairo.Context cr)
        {
            foreach (var child in sortedChildren)
            {
                PropagateDraw(child.Value, cr);
            }

            return true;
        }
    }
}
