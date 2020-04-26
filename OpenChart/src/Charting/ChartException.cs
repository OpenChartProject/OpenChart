using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// An exception used for chart-related errors. For example, trying to add two
    /// chart objects on the same beat and key.
    /// </summary>
    public class ChartException : Exception
    {
        public ChartException() : base() { }
        public ChartException(string msg) : base(msg) { }
        public ChartException(string msg, Exception inner) : base(msg, inner) { }
    }
}
