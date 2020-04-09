using System.IO;

namespace OpenChart.Formats
{
    /// <summary>
    /// An interface for classes which can read from and write to specific file formats.
    /// </summary>
    public interface IFileHandler<T>
    {
        /// <summary>
        /// The file extension used by this format. The string should always start with a period.
        /// </summary>
        static string FileExtension { get; }

        /// <summary>
        /// Reads in the raw file data and converts it to the format's corresponding chart class (type T).
        /// Classes that implement this should NOT close the stream.
        /// </summary>
        /// <param name="stream">The stream to read from (typically a FileStream).</param>
        T Read(StreamReader stream);

        /// <summary>
        /// Writes the chart to the provided stream.
        /// Classes that implement this should NOT close the stream.
        /// </summary>
        /// <param name="chart">The chart object.</param>
        /// <param name="stream">The stream to write to (typically a FileStream).</param>
        void Write(T chart, StreamWriter stream);
    }
}