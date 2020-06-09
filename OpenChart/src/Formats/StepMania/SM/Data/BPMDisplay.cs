using OpenChart.Formats.StepMania.SM.Enums;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// The display for the BPM.
    /// </summary>
    public class BPMDisplay
    {
        /// <summary>
        /// The min value of the display BPM.
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// The max value of the display BPM.
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// The display type.
        /// </summary>
        public DisplayBPMType Type { get; set; }

        /// <summary>
        /// Creates an instance for a fixed display.
        /// </summary>
        /// <param name="value">The BPM to display.</param>
        public static BPMDisplay NewFixedDisplay(int value)
        {
            return new BPMDisplay()
            {
                Min = value,
                Max = value,
                Type = DisplayBPMType.Fixed
            };
        }

        /// <summary>
        /// Creates an instance for a random display.
        /// </summary>
        public static BPMDisplay NewRandomDisplay()
        {
            return new BPMDisplay()
            {
                Type = DisplayBPMType.Random
            };
        }

        /// <summary>
        /// Creates an instance for a ranged display.
        /// </summary>
        /// <param name="min">The minimum BPM.</param>
        /// <param name="max">The maximum BPM.</param>
        public static BPMDisplay NewRangeDisplay(int min, int max)
        {
            return new BPMDisplay()
            {
                Min = min,
                Max = max,
                Type = DisplayBPMType.Range
            };
        }
    }
}
