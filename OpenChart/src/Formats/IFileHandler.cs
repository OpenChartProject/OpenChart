using System.IO;

namespace OpenChart.Formats
{
    /// <summary>
    /// An interface for classes which can read from and write to specific file formats.
    /// </summary>
    public interface IFileHandler<T>
    {
        /// <summary>
        /// Returns the file extension this handler supports (includes a period).
        /// </summary>
        string GetFileExtension();

        /// <summary>
        /// Reads in the raw file data and converts it to the format's corresponding chart class (type T).
        /// </summary>
        /// <param name="stream">The stream to read from (typically a FileStream).</param>
        T ReadData(StreamReader stream);

        /// <summary>
        /// Writes the chart to the provided stream.
        /// </summary>
        /// <param name="chart">The chart object to write to the stream.</param>
        /// <param name="stream">The stream to write to (typically a FileStream).</param>
        void WriteChart(T chart, StreamWriter stream);
    }
}