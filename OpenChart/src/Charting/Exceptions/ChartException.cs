using System;

namespace OpenChart.Charting.Exceptions
{
    /// <summary>
    /// An exception used for chart-related errors.
    /// </summary>
    public class ChartException : Exception
    {
        public ChartException(string msg = "") : base(msg) { }
    }
}
