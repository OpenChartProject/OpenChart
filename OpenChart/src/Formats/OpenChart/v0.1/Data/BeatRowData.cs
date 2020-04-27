using OpenChart.Charting.Properties;
using OpenChart.Formats.OpenChart.Version0_1.Objects;
using System;
using System.Linq;

namespace OpenChart.Formats.OpenChart.Version0_1.Data
{
    /// <summary>
    /// The data for the objects contained at a specific beat in the song.
    /// </summary>
    public class BeatRowData : IBeatObject, IComparable
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

        public BeatRowData() : this(0) { }

        public BeatRowData(int keyCount)
        {
            Beat = 0;
            Objects = new IChartObject[keyCount];
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            if (obj is BeatRowData data)
                return Beat.CompareTo(data.Beat);
            else
                throw new ArgumentException("Object is not a BeatRowData instance.");
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
