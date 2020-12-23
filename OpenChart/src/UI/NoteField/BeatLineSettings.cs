namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// The display settings for the BeatLines widget.
    /// </summary>
    public class BeatLineSettings
    {
        /// <summary>
        /// The color of a beat line.
        /// </summary>
        public Cairo.Color BeatLineColor { get; set; }

        /// <summary>
        /// The thickness (in pixels) of a beat line.
        /// </summary>
        public int BeatLineThickness { get; set; }

        /// <summary>
        /// The color for the start of a measure.
        /// </summary>
        public Cairo.Color MeasureLineColor { get; set; }

        /// <summary>
        /// The thickness (in pixels) for the start of a measure.
        /// </summary>
        public int MeasureLineThickness { get; set; }
    }
}
