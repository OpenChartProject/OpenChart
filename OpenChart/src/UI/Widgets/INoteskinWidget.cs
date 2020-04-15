using OpenChart.NoteSkins;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// An interface for a widget object whose appearance depends on a note skin.
    /// </summary>
    public interface ISkinnedWidget
    {
        /// <summary>
        /// The note skin the widget is using.
        /// </summary>
        KeyModeSkin NoteSkin { get; set; }
    }
}