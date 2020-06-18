namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// A single BPM change.
    /// </summary>
    public class BPM
    {
        /// <summary>
        /// The beat where the change occurs.
        /// </summary>
        public double Beat { get; set; }

        /// <summary>
        /// The BPM value.
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Creates a new BPM instance.
        /// </summary>
        public BPM(double beat, double value)
        {
            Beat = beat;
            Value = value;
        }
    }
}
