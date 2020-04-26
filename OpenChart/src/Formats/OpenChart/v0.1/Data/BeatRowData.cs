using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Linq;

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

        public BeatRowData()
        {
            Beat = 0;
            Objects = new IChartObject[] { };
        }

        public override bool Equals(object obj)
        {
            if (obj is BeatRowData data)
                return Beat.Equals(data.Beat) && Enumerable.SequenceEqual(Objects, data.Objects);

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Beat, Objects).GetHashCode();
        }
    }
}
