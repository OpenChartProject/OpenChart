using OpenChart.Charting.Exceptions;
using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a BPM change at a specific point in time.
    /// </summary>
    public class BPMInterval
    {
        /// <summary>
        /// The BPM of this interval.
        /// </summary>
        public readonly BPM BPM;

        /// <summary>
        /// The time this BPM changed occurred.
        /// </summary>
        public readonly double Seconds;

        /// <summary>
        /// Creates a new BPMInterval instance.
        /// </summary>
        public BPMInterval(BPM bpm, double seconds)
        {
            BPM = bpm;
            Seconds = seconds;
        }
    }

    /// <summary>
    /// Generates a list of BPMInterval objects for a list of BPM objects. The tracker listens
    /// to a BeatObjectList for changes so it knows when to regenerate the intervals.
    /// </summary>
    public class BPMIntervalTracker
    {
        bool hasChanged;
        BPMInterval[] _intervals;

        /// <summary>
        /// An array of intervals. This automatically updates itself when the object list has changed.
        /// </summary>
        public BPMInterval[] Intervals
        {
            get
            {
                if (hasChanged)
                    updateIntervals();

                return _intervals;
            }
            private set
            {
                _intervals = value;
            }
        }

        /// <summary>
        /// The object list of BPMs the tracker is using to generate intervals.
        /// </summary>
        public BeatObjectList<BPM> ObjectList { get; private set; }

        /// <summary>
        /// Creates a new BPMTimeTracker instance.
        /// </summary>
        /// <param name="objectList">The BeatObjectList that will be watched for changes.</param>
        public BPMIntervalTracker(BeatObjectList<BPM> objectList)
        {
            if (objectList == null)
                throw new ArgumentNullException("Object list cannot be null.");

            hasChanged = true;
            ObjectList = objectList;

            // Flag the intervals as needing to be changed when the list updates.
            objectList.Added += delegate { hasChanged = true; };
            objectList.Cleared += delegate { hasChanged = true; };
            objectList.Removed += delegate { hasChanged = true; };
        }

        /// <summary>
        /// Returns the time in seconds that the given beat occurs at.
        /// </summary>
        public double BeatToTime(Beat beat)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (beat.Value == 0)
                return 0;

            BPMInterval cur = Intervals[0];
            BPMInterval next = cur;

            for (var i = 0; i < Intervals.Length; i++)
            {
                if (i == Intervals.Length - 1)
                    break;

                cur = Intervals[i];
                next = Intervals[i + 1];

                // This beat occurs between these two intervals.
                if (cur.BPM.Beat.Value <= beat.Value && beat.Value < next.BPM.Beat.Value)
                    break;
            }

            return cur.Seconds + (beat.Value - cur.BPM.Beat.Value) * cur.BPM.SecondsPerBeat();
        }

        /// <summary>
        /// Returns the beat that occurs at the given time in seconds.
        /// </summary>
        /// <param name="seconds">Time in seconds.</param>
        public Beat TimeToBeat(double seconds)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (seconds == 0)
                return 0;

            BPMInterval cur = Intervals[0];
            BPMInterval next = cur;

            for (var i = 0; i < Intervals.Length; i++)
            {
                if (i == Intervals.Length - 1)
                    break;

                cur = Intervals[i];
                next = Intervals[i + 1];

                // This beat occurs between these two intervals.
                if (cur.Seconds <= seconds && seconds < next.Seconds)
                    break;
            }

            return cur.BPM.Beat.Value + (seconds - cur.Seconds) * cur.BPM.BeatsPerSecond();
        }

        /// <summary>
        /// Returns the index of the interval which occurs at the given time in seconds.
        /// </summary>
        /// <param name="seconds">Time in seconds.</param>
        public int GetIndexAtTime(double seconds)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (seconds < 0)
                throw new ArgumentOutOfRangeException("Time cannot be negative.");

            for (var i = 0; i < Intervals.Length - 1; i++)
            {
                // This time occurs between these two intervals.
                if (Intervals[i].Seconds <= seconds && seconds < Intervals[i + 1].Seconds)
                    return i;
            }

            // The time occurs after the last interval change.
            return Intervals.Length - 1;
        }

        /// <summary>
        /// Updates the Intervals array to reflect the new state of the object list.
        /// This is only triggered when a caller tries to retrieve the Intervals property
        /// and there has been a change since the last retrieval.
        /// </summary>
        private void updateIntervals()
        {
            var bpms = ObjectList.ToArray();

            BPMInterval curInterval = null;
            BPMInterval lastInterval = null;
            List<BPMInterval> intervalList = new List<BPMInterval>();

            foreach (var bpm in bpms)
            {
                if (lastInterval == null)
                {
                    if (bpm.Beat.Value != 0)
                        throw new NoBPMAtBeatZeroException();

                    curInterval = new BPMInterval(bpm, 0);
                }
                else
                {
                    // Calculate how much time has elapsed since the last BPM change.
                    var beatDelta = bpm.Beat.Value - lastInterval.BPM.Beat.Value;
                    var timeDelta = beatDelta * lastInterval.BPM.SecondsPerBeat();

                    // Add the elapsed time to track the total time needed to reach this
                    // BPM change if you started at beat 0.
                    var elapsed = lastInterval.Seconds + timeDelta;

                    curInterval = new BPMInterval(bpm, elapsed);
                }

                intervalList.Add(curInterval);
                lastInterval = curInterval;
            }

            Intervals = intervalList.ToArray();
            hasChanged = false;
        }
    }
}
