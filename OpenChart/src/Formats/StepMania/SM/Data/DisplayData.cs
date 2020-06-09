namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Data used for displaying the step file.
    /// </summary>
    public class DisplayData
    {
        /// <summary>
        /// The path to the banner image.
        /// Field: #BANNER
        /// </summary>
        public string Banner { get; set; }

        /// <summary>
        /// The path to the background image.
        /// Field: #BACKGROUND
        /// </summary>
        public string Background { get; set; }

        /// <summary>
        /// The BPM value to display.
        /// FIELD: #DISPLAYBPM
        /// </summary>
        public BPMDisplay BPMDisplay { get; set; }

        /// <summary>
        /// The path to the CD title image.
        /// Field: #CDTITLE
        /// </summary>
        public string CDTitle { get; set; }

        /// <summary>
        /// Whether the song is visible on the song wheel or not.
        /// Field: #SELECTABLE
        /// </summary>
        public bool Selectable { get; set; }

        /// <summary>
        /// The background changes for the song. (not supported)
        /// Field: #BGCHANGES
        /// </summary>
        public string BackgroundChanges { get; set; }

        /// <summary>
        /// The foreground changes for the song. (not supported)
        /// Field: #BGCHANGES
        /// </summary>
        public string ForegroundChanges { get; set; }
    }
}
