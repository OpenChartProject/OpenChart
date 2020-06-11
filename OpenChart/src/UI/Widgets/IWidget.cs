namespace OpenChart.UI
{
    /// <summary>
    /// Interface for a class that returns a Gtk widget.
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// Returns a Gtk widget.
        /// </summary>
        Gtk.Widget GetWidget();
    }
}
