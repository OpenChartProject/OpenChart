using OpenChart.Charting.Exceptions;
using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.Charting
{
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
        /// <param name="seconds">The beat to get the timestamp of.</param>
        /// <param name="fromIndex">An optional start index (if it's known).</param>
        public Time BeatToTime(Beat beat, uint fromIndex = 0)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (fromIndex >= Intervals.Length)
                throw new ArgumentOutOfRangeException("fromIndex is out of range.");
            else if (beat.Value == 0)
                return 0;

            BPMInterval cur = Intervals[fromIndex];
            BPMInterval next = cur;

            for (var i = fromIndex; i < Intervals.Length; i++)
            {
                cur = Intervals[i];

                if (i == Intervals.Length - 1)
                    break;

                next = Intervals[i + 1];

                // This beat occurs between these two intervals.
                if (cur.BPM.Beat.Value <= beat.Value && beat.Value < next.BPM.Beat.Value)
                    break;
            }

            return cur.BeatToTime(beat);
        }

        /// <summary>
        /// Returns the index of the interval which occurs at the given time in seconds.
        /// </summary>
        /// <param name="seconds">Time in seconds.</param>
        /// <param name="fromIndex">An optional start index (if it's known).</param>
        public uint GetIndexAtTime(Time time, uint fromIndex = 0)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (fromIndex >= Intervals.Length)
                throw new ArgumentOutOfRangeException("fromIndex is out of range.");
            else if (fromIndex == Intervals.Length - 1)
                return (uint)Intervals.Length - 1;

            for (var i = fromIndex; i < Intervals.Length - 1; i++)
            {
                // This time occurs between these two intervals.
                if (Intervals[i].Time.Value <= time.Value && time.Value < Intervals[i + 1].Time.Value)
                    return (uint)i;
            }

            // The time occurs after the last interval change.
            return (uint)Intervals.Length - 1;
        }

        /// <summary>
        /// Returns an iterator that yields the time (in seconds) of each successive beat, starting
        /// at the given `startTime`.
        /// </summary>
        /// <param name="startTime">The time to start at.</param>
        /// <param name="fromIndex">An optional start index (if it's known).</param>
        public IEnumerable<BeatTime> GetBeats(Time startTime, uint fromIndex = 0)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (fromIndex >= Intervals.Length)
                throw new ArgumentOutOfRangeException("fromIndex is out of range.");

            var index = GetIndexAtTime(startTime, fromIndex);
            Time time = startTime;

            // Get the beat we're starting on.
            Beat beat = Intervals[index].TimeToBeat(time);
            beat.Value = Math.Ceiling(beat.Value);

            while (true)
            {
                time = BeatToTime(beat, index);

                yield return new BeatTime(beat, time);

                index = GetIndexAtTime(time, index);

                // This only yields on whole beat values.
                beat.Value++;
            }
        }

        /// <summary>
        /// Returns the beat that occurs at the given time in seconds.
        /// </summary>
        /// <param name="seconds">Time in seconds.</param>
        /// <param name="fromIndex">An optional start index (if it's known).</param>
        public Beat TimeToBeat(Time time, uint fromIndex = 0)
        {
            if (Intervals.Length == 0)
                throw new Exception("The Intervals array is empty.");
            else if (fromIndex >= Intervals.Length)
                throw new ArgumentOutOfRangeException("fromIndex is out of range.");
            else if (time.Value == 0)
                return 0;

            BPMInterval cur = Intervals[fromIndex];
            BPMInterval next = cur;

            for (var i = fromIndex; i < Intervals.Length; i++)
            {
                cur = Intervals[i];

                if (i == Intervals.Length - 1)
                    break;

                next = Intervals[i + 1];

                // This beat occurs between these two intervals.
                if (cur.Time.Value <= time.Value && time.Value < next.Time.Value)
                    break;
            }

            return cur.TimeToBeat(time);
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
                    var elapsed = lastInterval.Time.Value + timeDelta;

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
