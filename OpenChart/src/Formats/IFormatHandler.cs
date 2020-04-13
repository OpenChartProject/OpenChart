using OpenChart.Projects;
using System.IO;

namespace OpenChart.Formats
{
    /// <summary>
    /// The interface for a class which can read and write to a specific file format.
    /// </summary>
    public interface IFormatHandler
    {
        /// <summary>
        /// The name of the file format.
        /// </summary>
        string FormatName { get; }

        /// <summary>
        /// The extension the file format uses. Includes a period.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Reads the data from a specific file format and returns a Project object.
        /// </summary>
        /// <param name="reader">The stream that contains the file data.</param>
        Project Read(StreamReader reader);

        /// <summary>
        /// Writes the chart to the stream using a specific file format.
        /// </summary>
        /// <param name="writer">The stream to write to.</param>
        /// <param name="chart">The chart to write.</param>
        void Write(StreamWriter writer, Project chart);
    }
}