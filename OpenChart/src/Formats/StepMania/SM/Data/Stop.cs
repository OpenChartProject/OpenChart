namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents a stop. Stops freeze the chart for a period of time.
    /// </summary>
    public class Stop
    {
        /// <summary>
        /// The beat the stop occurs.
        /// </summary>
        public double Beat { get; set; }

        /// <summary>
        /// The length of the stop, in seconds.
        /// </summary>
        public double Seconds { get; set; }

        /// <summary>
        /// Creates a new Stop instance.
        /// </summary>
        public Stop(double beat, double seconds)
        {
            Beat = beat;
            Seconds = seconds;
        }
    }
}
