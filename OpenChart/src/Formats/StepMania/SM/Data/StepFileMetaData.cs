namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Metadata about the step file that doesn't fit into the other data classes.
    /// </summary>
    public class StepFileMetaData
    {
        /// <summary>
        /// The author or pack name.
        /// Field: #CREDIT
        /// </summary>
        public string Credit { get; set; }
    }
}
