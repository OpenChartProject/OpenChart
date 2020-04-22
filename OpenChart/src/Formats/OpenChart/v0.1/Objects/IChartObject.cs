using System;

namespace OpenChart.Formats.OpenChart.Version0_1.Objects
{
    /// <summary>
    /// An interface for chart objects which are identified by a unique type string.
    /// </summary>
    public interface IChartObject
    {
        /// <summary>
        /// A unique string that describes the chart object class.
        /// </summary>
        string Type { get; }
    }
}