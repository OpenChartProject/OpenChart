using OpenChart.Formats.StepMania.SM.Enums;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// The display for the BPM.
    /// </summary>
    public class DisplayBPM
    {
        /// <summary>
        /// The min value of the display BPM.
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// The max value of the display BPM.
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// The display type.
        /// </summary>
        public DisplayBPMType Type { get; set; }

        /// <summary>
        /// Creates an instance for a fixed display.
        /// </summary>
        /// <param name="value">The BPM to display.</param>
        public static DisplayBPM NewFixedDisplay(double value)
        {
            return new DisplayBPM()
            {
                Min = value,
                Max = value,
                Type = DisplayBPMType.Fixed
            };
        }

        /// <summary>
        /// Creates an instance for a random display.
        /// </summary>
        public static DisplayBPM NewRandomDisplay()
        {
            return new DisplayBPM()
            {
                Type = DisplayBPMType.Random
            };
        }

        /// <summary>
        /// Creates an instance for a ranged display.
        /// </summary>
        /// <param name="min">The minimum BPM.</param>
        /// <param name="max">The maximum BPM.</param>
        public static DisplayBPM NewRangeDisplay(double min, double max)
        {
            return new DisplayBPM()
            {
                Min = min,
                Max = max,
                Type = DisplayBPMType.Range
            };
        }
    }
}
