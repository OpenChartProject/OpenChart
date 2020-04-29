using OpenChart.Charting.Properties;

namespace OpenChart.Charting
{
    public class BPMList
    {
        public BeatObjectList<BPM> BPMs { get; private set; }
        public BPMTimeTracker Time { get; private set; }

        public BPMList(BeatObjectList<BPM> bpms)
        {
            BPMs = bpms;
            Time = new BPMTimeTracker(BPMs);
        }
    }
}
