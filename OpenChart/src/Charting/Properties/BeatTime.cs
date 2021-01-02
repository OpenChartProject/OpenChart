namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// Represents a beat that occurs at a specific time.
    /// </summary>
    public class BeatTime : IBeatObject, ITimedObject
    {
        public Beat Beat { get; set; }
        public Time Time { get; set; }

        /// <summary>
        /// Creates a new BeatTime instance.
        /// </summary>
        public BeatTime(Beat beat, Time time)
        {
            Beat = beat;
            Time = time;
        }

        public override string ToString()
        {
            return string.Format("BeatTime(beat={0} time={1})", Beat.ToString(), Time.ToString());
        }
    }
}
