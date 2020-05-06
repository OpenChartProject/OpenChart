using OpenChart.Charting.Objects;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// An interface for an object which can be placed onto the note field, specifically
    /// a note field key.
    /// </summary>
    public interface INoteFieldChartObject
    {
        /// <summary>
        /// The widget for this object. Objects that are composed of multiple widgets should
        /// wrap them in a container.
        /// </summary>
        Gtk.Widget GetWidget();

        /// <summary>
        /// The native chart object that has the data this note field widget is displaying.
        /// </summary>
        BaseObject GetChartObject();

        /// <summary>
        /// The offset to the center of the widget, in pixels. This is used to center widgets
        /// on to beat lines.
        ///
        /// For example, if the widget is 128 pixels tall, the center offset would be half
        /// its height, or 64 pixels.
        /// </summary>
        int GetWidgetCenterOffset();

        /// <summary>
        /// Prompts the object to update its appearance.
        /// </summary>
        void Update() { }
    }
}
