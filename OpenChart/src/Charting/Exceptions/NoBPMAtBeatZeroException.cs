namespace OpenChart.Charting.Exceptions
{
    /// <summary>
    /// An exception raised when there is no BPM defined at the first beat.
    /// </summary>
    public class NoBPMAtBeatZeroException : ChartException
    {
        public NoBPMAtBeatZeroException(string msg = "") : base(msg) { }
    }
}
