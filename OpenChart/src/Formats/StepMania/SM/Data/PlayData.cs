using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Data that affects how the step file is played.
    /// </summary>
    public class PlayData
    {
        /// <summary>
        /// The music offset for the start of a chart.
        /// Field: #OFFSET
        /// </summary>
        public double Offset { get; set; }

        /// <summary>
        /// The BPM changes.
        /// Field: #BPMS
        /// </summary>
        public List<BPM> BPMs { get; set; }

        /// <summary>
        /// The stops.
        /// Field: #STOPS
        /// </summary>
        public List<Stop> Stops { get; set; }

        public PlayData()
        {
            BPMs = new List<BPM>();
            Stops = new List<Stop>();
        }
    }
}
