namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// An interface for objects that are associated with a particular time.
    /// </summary>
    public interface ITimedObject
    {
        Time Time { get; }
    }
}
