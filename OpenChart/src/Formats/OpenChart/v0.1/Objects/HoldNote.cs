using OpenChart.Charting.Properties;

namespace OpenChart.Formats.OpenChart.Version0_1.Objects
{
    /// <summary>
    /// Represents a hold note in the chart.
    /// </summary>
    public class HoldNote : IBeatDurationObject, IChartObject
    {
        /// <summary>
        /// The duration of the hold note.
        /// </summary>
        public BeatDuration BeatDuration { get; set; }
        public string Type => ChartObjectType.HoldNote;

        public HoldNote(BeatDuration duration)
        {
            BeatDuration = duration;
        }
    }
}