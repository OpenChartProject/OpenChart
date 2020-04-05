using System;

namespace OpenChart.Charting
{
    public static class ChartUtils
    {
        /// <summary>
        /// Calculates the time in seconds where a specific beat occurs given a list
        /// of BPM changes. Due to floating point rounding, the time returned may be
        /// slightly inaccurate.
        /// </summary>
        /// <param name="bpms">The BPM changes. Cannot be empty.</param>
        /// <param name="beat">The beat number. Must be greater than zero.</param>
        /// <returns>The time, in seconds.</returns>
        public static double BeatToSeconds(BPM[] bpms, double beat)
        {
            if (bpms.Length == 0)
            {
                throw new ArgumentException("BPMs cannot be empty.");
            }
            else if (beat < 0)
            {
                throw new ArgumentException("Beat cannot be negative.");
            }
            else if (beat == 0)
            {
                return 0;
            }

            double elapsed = 0;
            BPM lastBPM = null;

            foreach (var bpm in bpms)
            {
                // The first BPM change is at 0:00 so no time has elapsed.
                if (lastBPM == null)
                {
                    lastBPM = bpm;
                    continue;
                }

                // Get the number of beats since the last BPM change up until
                // the beat we're interested in or the current BPM change,
                // whichever comes first.
                var delta = Math.Min(beat, bpm.Beat) - lastBPM.Beat;

                // Multiply how long each beat lasts for with the number of beats.
                elapsed += (60 / lastBPM.Value) * delta;

                // We've gone as far as we need.
                if (lastBPM.Beat >= beat)
                {
                    return elapsed;
                }

                lastBPM = bpm;
            }

            // The beat came after the last BPM change, so add whatever is remaining.
            elapsed += (60 / lastBPM.Value) * (beat - lastBPM.Beat);

            return elapsed;
        }

        /// <summary>
        /// Calculates what beat would occur at the given time given a list of
        /// BPM changes. Due to floating point rounding, the beat returned may be
        /// slightly inaccurate (consider quantanizing to a known beat snap).
        /// </summary>
        /// <param name="bpms">The BPM changes. Cannot be empty.</param>
        /// <param name="seconds">The time in seconds. Cannot be negative.</param>
        /// <returns>The beat number.</returns>
        public static double SecondsToBeat(BPM[] bpms, double seconds)
        {
            if (bpms.Length == 0)
            {
                throw new ArgumentException("BPMs cannot be empty.");
            }
            else if (seconds < 0)
            {
                throw new ArgumentException("Seconds cannot be negative.");
            }
            else if (seconds == 0)
            {
                return 0;
            }

            double elapsed = 0;
            BPM lastBPM = null;

            foreach (var bpm in bpms)
            {
                // The first BPM change is always at beat 0.
                if (lastBPM == null)
                {
                    lastBPM = bpm;
                    continue;
                }

                // Get how much time has elapsed since the last BPM change.
                var beatDelta = bpm.Beat - lastBPM.Beat;
                var timeDelta = (60 / lastBPM.Value) * beatDelta;

                if (elapsed + timeDelta == seconds)
                {
                    return bpm.Beat;
                }
                else if (elapsed + timeDelta > seconds)
                {
                    // Calculate how much we overshot our target time.
                    var overshot = (elapsed + timeDelta) - seconds;

                    // If 100% is too much, calculate the target percentage by subtracting out
                    // the amount that was overshot.
                    overshot = 1 - (overshot / timeDelta);

                    return lastBPM.Beat + (beatDelta * overshot);
                }

                elapsed += timeDelta;
                lastBPM = bpm;
            }

            // The target beat came after the last BPM change. Calculate how much time is remaining
            // and multiply that by the beats per second.
            var remaining = seconds - elapsed;

            return lastBPM.Beat + (remaining * lastBPM.Value / 60);
        }
    }
}