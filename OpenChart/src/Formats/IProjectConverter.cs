using OpenChart.Projects;

namespace OpenChart.Formats
{
    /// <summary>
    /// The interface for a class which can convert between the native Project class
    /// and a FFO (file-format object) class.
    /// </summary>
    /// <typeparam name="T">The FFO class.</typeparam>
    public interface IProjectConverter<T>
    {
        /// <summary>
        /// Whether this converter supports exporting multiple charts at one time.
        /// </summary>
        static bool SupportsMultipleExports { get; }

        /// <summary>
        /// Converts a FFO to a native Project.
        /// </summary>
        /// <param name="chart">A FFO (file-format object).</param>
        Project ToNative(T chart);

        /// <summary>
        /// Converts a native Project to a FFO.
        /// </summary>
        /// <param name="chart">A native Project.</param>
        T FromNative(Project chart);
    }
}