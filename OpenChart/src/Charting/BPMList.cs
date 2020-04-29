using OpenChart.Charting.Properties;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a list of BPM objects that is bundled with a time tracker.
    /// </summary>
    public class BPMList
    {
        /// <summary>
        /// The list of BPM changes.
        /// </summary>
        public BeatObjectList<BPM> BPMs { get; private set; }

        /// <summary>
        /// A time tracker. This is setup to watch the BPMs object for changes.
        /// </summary>
        public BPMTimeTracker Time { get; private set; }

        /// <summary>
        /// Creates a new BPMList instance.
        /// </summary>
        public BPMList(BeatObjectList<BPM> bpms)
        {
            BPMs = bpms;
            Time = new BPMTimeTracker(BPMs);
        }
    }
}
