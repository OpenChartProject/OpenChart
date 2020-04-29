using OpenChart.Charting.Properties;
using System.Collections.Generic;

namespace OpenChart.Charting
{
    public class BPMInterval
    {
        public readonly BPM BPM;
        public readonly double Seconds;

        public BPMInterval(BPM bpm, double seconds)
        {
            BPM = bpm;
            Seconds = seconds;
        }
    }

    public class BPMTimeTracker
    {
        BeatObjectList<BPM> watchList;
        bool hasChanged;

        BPMInterval[] _intervals;
        public BPMInterval[] Intervals
        {
            get
            {
                if (hasChanged)
                    Update();

                return _intervals;
            }
            private set
            {
                _intervals = value;
            }
        }

        public BPMTimeTracker(BeatObjectList<BPM> watchList)
        {
            hasChanged = true;

            watchList.Added += delegate { hasChanged = true; };
            watchList.Cleared += delegate { hasChanged = true; };
            watchList.Removed += delegate { hasChanged = true; };
        }

        public void Update()
        {
            var bpms = watchList.ToArray();

            BPMInterval curInterval = null;
            BPMInterval lastInterval = null;
            List<BPMInterval> intervalList = new List<BPMInterval>();

            foreach (var bpm in bpms)
            {
                if (lastInterval == null)
                    curInterval = new BPMInterval(bpm, 0);
                else
                {
                    var beatDelta = bpm.Beat.Value - lastInterval.BPM.Beat.Value;
                    var timeDelta = beatDelta * lastInterval.BPM.SecondsPerBeat();
                    var elapsed = lastInterval.Seconds + timeDelta;

                    curInterval = new BPMInterval(lastInterval.BPM, elapsed);
                }

                intervalList.Add(lastInterval);
                lastInterval = curInterval;
            }

            Intervals = intervalList.ToArray();
            hasChanged = false;
        }
    }
}
