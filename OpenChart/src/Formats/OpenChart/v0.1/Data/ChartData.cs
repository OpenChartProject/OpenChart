using OpenChart.Charting.Properties;
using System;
using System.Linq;
using System.Text.Json.Serialization;

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

        /// <summary>
        /// The beat rows for the chart.
        /// </summary>
        public BeatRowData[] Rows { get; set; }

        /// <summary>
        /// The BPM changes for the chart
        /// </summary>
        [JsonPropertyName("bpms")]
        public BPM[] BPMs { get; set; }

        public ChartData()
        {
            // Default to an empty array instead of null if it's not set.
            Rows = new BeatRowData[] { };
            BPMs = new BPM[] { };
        }

        public override bool Equals(object obj)
        {
            if (obj is ChartData data)
                return (
                    KeyCount.Equals(data.KeyCount) &&
                    Author == data.Author &&
                    ChartName == data.ChartName &&
                    Enumerable.SequenceEqual(Rows, data.Rows) &&
                    Enumerable.SequenceEqual(BPMs, data.BPMs)
                );

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(KeyCount, Author, ChartName, Rows, BPMs).GetHashCode();
        }
    }
}
