using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// A chart represents a playable beat mapping for a song. The chart has a key count
    /// which dictates what keymode it's played in.
    ///
    /// In the context of file formats, this class is referred to the "native" chart class.
    /// </summary>
    public class Chart
    {
        public BPMList BPMList { get; private set; }

        /// <summary>
        /// The key count for the chart.
        /// </summary>
        public KeyCount KeyCount;

        /// <summary>
        /// The chart objects that make up the chart. Chart objects include things like tap notes,
        /// holds, mines, etc.
        ///
        /// The objects are stored as an array of linked lists. Each list element represents a key/column.
        /// </summary>
        public BeatObjectList<BaseObject>[] Objects { get; private set; }

        /// <summary>
        /// Creates a new chart instance.
        /// </summary>
        /// <param name="keyCount">The keycount of the chart.</param>
        public Chart(KeyCount keyCount)
        {
            KeyCount = keyCount;
            BPMList = new BPMList(new BeatObjectList<BPM>());
            Objects = new BeatObjectList<BaseObject>[KeyCount.Value];

            for (var i = 0; i < KeyCount.Value; i++)
            {
                Objects[i] = new BeatObjectList<BaseObject>();
            }
        }

        public override bool Equals(object obj)
        {
            var chart = obj as Chart;

            if (chart == null)
                return false;

            return (
                KeyCount == chart.KeyCount
                && BPMList.Equals(chart.BPMList)
                && Objects.Equals(chart.Objects)
            );
        }

        public override int GetHashCode()
        {
            return Tuple.Create(KeyCount, BPMList, Objects).GetHashCode();
        }
    }
}
