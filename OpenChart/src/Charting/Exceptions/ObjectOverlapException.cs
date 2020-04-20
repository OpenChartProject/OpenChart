namespace OpenChart.Charting.Exceptions
{
    /// <summary>
    /// An exception raised when two objects overlap.
    /// </summary>
    public class ObjectOverlapException : ChartException
    {
        public ObjectOverlapException(string msg = "") : base(msg) { }
    }
}
