namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents all of the data stored in a .sm file.
    /// </summary>
    public class StepFileData
    {
        /// <summary>
        /// The song data.
        /// </summary>
        public SongData SongData { get; set; }

        /// <summary>
        /// Metadata about the step file.
        /// </summary>
        public StepFileMetaData MetaData { get; set; }

        /// <summary>
        /// Graphical data for the step file.
        /// </summary>
        public GraphicData GraphicData { get; set; }
    }
}
