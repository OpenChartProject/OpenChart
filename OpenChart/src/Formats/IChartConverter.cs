using OpenChart.Charting;

namespace OpenChart.Formats
{
    /// <summary>
    /// The interface for converting between the native Chart class and a
    /// format-specific chart class (type T).
    /// </summary>
    public interface IChartConverter<T>
    {
        /// <summary>
        /// Converts a format-specific chart to a native Chart.
        /// </summary>
        /// <param name="chart">A format-specific chart.</param>
        Chart ToNative(T chart);

        /// <summary>
        /// Converts a native Chart to a format-specific chart.
        /// </summary>
        /// <param name="chart">A native Chart.</param>
        T FromNative(Chart chart);
    }
}