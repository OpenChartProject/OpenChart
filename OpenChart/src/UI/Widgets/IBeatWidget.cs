namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// An interface for a widget which is bound to a specific beat.
    /// </summary>
    public interface IBeatWidget
    {
        /// <summary>
        /// The beat the widget is bound to. The beat determines the y-position of the
        /// widget in the note field.
        /// </summary>
        double Beat { get; set; }
    }
}
