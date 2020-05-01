using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Formats.OpenChart.Version0_1.Objects
{
    /// <summary>
    /// Represents a hold note in the chart.
    /// </summary>
    public class HoldNote : IChartObject, IBeatDurationObject
    {
        /// <summary>
        /// The duration of the hold note.
        /// </summary>
        public BeatDuration Length { get; set; }

        public string Type => ChartObjectType.HoldNote;

        public override bool Equals(object obj)
        {
            if (obj is HoldNote note)
                return note.Length.Equals(Length);

            return false;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Type, Length).GetHashCode();
        }
    }
}
