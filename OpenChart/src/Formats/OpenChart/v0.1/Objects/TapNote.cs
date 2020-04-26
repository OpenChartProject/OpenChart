namespace OpenChart.Formats.OpenChart.Version0_1.Objects
{
    /// <summary>
    /// Represents a tap note in the chart.
    /// </summary>
    public class TapNote : IChartObject
    {
        public string Type => ChartObjectType.TapNote;
    }
}