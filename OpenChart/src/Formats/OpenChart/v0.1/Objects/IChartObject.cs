namespace OpenChart.Formats.OpenChart.Version0_1.Objects
{
    /// <summary>
    /// An interface for objects that contain a Type field.
    /// </summary>
    public interface IChartObject
    {
        /// <summary>
        /// A string describing the object's type. This should be constant.
        /// </summary>
        string Type { get; }
    }
}