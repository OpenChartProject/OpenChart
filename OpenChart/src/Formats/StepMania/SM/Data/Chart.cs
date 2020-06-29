using OpenChart.Formats.StepMania.SM.Enums;
using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents a chart in a step file.
    /// </summary>
    public class Chart
    {
        /// <summary>
        /// A string describing what type of chart this is, e.g. 'dance-single'. I chose not to
        /// make this an enum just because there are so many different types and they aren't
        /// documented well (or at all).
        /// </summary>
        public string ChartType { get; set; }

        /// <summary>
        /// The author field.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The chart's difficulty. This is what the user scrolls through to select the difficulty.
        /// </summary>
        public ChartDifficulty Difficulty { get; set; }

        /// <summary>
        /// The numeric difficulty. For example, if a chart is listed as an "18" on the song wheel.
        /// </summary>
        public int DifficultyRating { get; set; }

        /// <summary>
        /// Used by old versions of sm. Unused.
        /// </summary>
        public string GrooveRadarValues { get; set; }

        /// <summary>
        /// The measures in the chart. This holds the note data.
        /// </summary>
        public List<Measure> Measures { get; set; }

        /// <summary>
        /// Creates a new Chart instance.
        /// </summary>
        public Chart()
        {
            Measures = new List<Measure>();
        }
    }
}
