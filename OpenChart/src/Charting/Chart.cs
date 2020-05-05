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
        public KeyCount KeyCount { get; private set; }

        /// <summary>
        /// The chart's author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The chart's name. This is what the difficulty is named in osu!/Quaver.
        /// </summary>
        public string ChartName { get; set; }

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
                Objects[i].Added += (o, e) => updateObjectTime(e.Object);
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

        /// <summary>
        /// Calculates the time that the object occurs in the chart, given the current
        /// list of BPMs.
        /// </summary>
        /// <param name="obj">The object to update.</param>
        private void updateObjectTime(BaseObject obj)
        {
            obj.Time = BPMList.Time.BeatToTime(obj.Beat);

            if (obj is BaseLongObject longObject)
                longObject.Duration = BPMList.Time.BeatToTime(longObject.Beat.Value + longObject.Length.Value);
        }
    }
}
