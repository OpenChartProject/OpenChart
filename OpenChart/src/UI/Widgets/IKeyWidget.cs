using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// An interface for a note field widget that corresponds to a specific key.
    /// </summary>
    public interface IKeyWidget
    {
        /// <summary>
        /// The key the widget is bound to. The key determines the x-position of the
        /// widget in the note field.
        /// </summary>
        int Key { get; set; }
    }
}
