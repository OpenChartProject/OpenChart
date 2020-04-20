using OpenChart.Charting.Properties;

namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    /// <summary>
    /// The data for an individual chart in a project.
    /// </summary>
    public class ChartData
    {
        /// <summary>
        /// The chart's keycount.
        /// </summary>
        public KeyCount KeyCount { get; set; }

        /// <summary>
        /// The name of the chart's author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The chart's name.
        /// </summary>
        public string ChartName { get; set; }
    }
}
