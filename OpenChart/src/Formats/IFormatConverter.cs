using OpenChart.Charting;

namespace OpenChart.Formats
{
    /// <summary>
    /// The interface for a class which can convert between the native Chart class
    /// and a FFO (file-format object) class.
    /// </summary>
    /// <typeparam name="T">The FFO class.</typeparam>
    public interface IFormatConverter<T>
    {
        /// <summary>
        /// Converts a FFO to a native Chart.
        /// </summary>
        /// <param name="chart">A FFO (file-format object).</param>
        Chart ToNative(T chart);

        /// <summary>
        /// Converts a native Chart to a FFO.
        /// </summary>
        /// <param name="chart">A native Chart.</param>
        T FromNative(Chart chart);
    }
}