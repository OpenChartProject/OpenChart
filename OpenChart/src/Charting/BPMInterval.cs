using OpenChart.Charting.Properties;
using System;

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
        public readonly Time Time;

        /// <summary>
        /// Creates a new BPMInterval instance.
        /// </summary>
        public BPMInterval(BPM bpm, Time time)
        {
            BPM = bpm;
            Time = time;
        }

        /// <summary>
        /// Returns the beat that occurs at the given time for this BPM interval.
        /// </summary>
        /// <param name="targetTime">The time of the beat.</param>
        public Time BeatToTime(Beat beat)
        {
            if (beat.Value < BPM.Beat.Value)
                throw new ArgumentOutOfRangeException("The beat must be after the BPM change occurs.");

            return Time.Value + (beat.Value - BPM.Beat.Value) * BPM.SecondsPerBeat();
        }

        /// <summary>
        /// Returns the beat that occurs at the given time for this BPM interval.
        /// </summary>
        /// <param name="targetTime">The time of the beat.</param>
        public Beat TimeToBeat(Time targetTime)
        {
            if (targetTime.Value < Time.Value)
                throw new ArgumentOutOfRangeException("The time must be after the BPM change occurs.");

            return BPM.Beat.Value + (targetTime.Value - Time.Value) * BPM.BeatsPerSecond();
        }
    }
}
