namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents all of the data stored in a .sm file.
    /// </summary>
    public class StepFileData
    {
        /// <summary>
        /// Display data for the step file.
        /// </summary>
        public DisplayData DisplayData { get; set; }

        /// <summary>
        /// Metadata about the step file.
        /// </summary>
        public StepFileMetaData MetaData { get; set; }

        /// <summary>
        /// Play data for the step file.
        /// </summary>
        /// <value></value>
        public PlayData PlayData { get; set; }

        /// <summary>
        /// The song data.
        /// </summary>
        public SongData SongData { get; set; }
    }
}
