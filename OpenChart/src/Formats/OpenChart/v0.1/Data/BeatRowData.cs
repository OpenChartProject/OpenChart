using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.Objects;

namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    /// <summary>
    /// The data for the objects contained at a specific beat in the song.
    /// </summary>
    public class BeatRowData : IBeatObject
    {
        /// <summary>
        /// The beat this row is for.
        /// </summary>
        public Beat Beat { get; set; }

        /// <summary>
        /// The objects contained in this row. Each element in this array corresponds
        /// to a key index, where the size of the array is the key count of the chart.
        /// </summary>
        public IChartObject[] Objects { get; set; }
    }
}