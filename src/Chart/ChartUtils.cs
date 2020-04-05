namespace OpenChart.Chart
{
    public static class ChartUtils
    {
        /// <summary>
        /// Calculates the time in seconds where a specific beat occurs given a list
        /// of BPM changes.
        /// </summary>
        /// <param name="bpms">The BPM changes. Cannot be empty.</param>
        /// <param name="beat">The beat number. Must be greater than zero.</param>
        /// <returns>The time, in seconds.</returns>
        public static float BeatToSeconds(BPM[] bpms, float beat)
        {
            return 0;
        }

        /// <summary>
        /// Calculates what beat would occur at the given time given a list of
        /// BPM changes.
        /// </summary>
        /// <param name="bpms">The BPM changes. Cannot be empty.</param>
        /// <param name="seconds">The time in seconds. Cannot be negative.</param>
        /// <returns>The beat number.</returns>
        public static float SecondsToBeat(BPM[] bpms, float seconds)
        {
            return 0;
        }
    }
}