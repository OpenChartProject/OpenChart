using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// An event bus that wraps a Chart object. This provides a single source of truth for
    /// events fired by a Chart and its components.
    /// </summary>
    public class ChartEvents
    {
        /// <summary>
        /// The chart object being watched.
        /// </summary>
        public readonly Chart Chart;

        public event EventHandler BPMAdded;
        public event EventHandler BPMChanged;
        public event EventHandler BPMDeleted;

        public event EventHandler ObjectAdded;
        public event EventHandler ObjectDeleted;

        public ChartEvents(Chart chart)
        {
            Chart = chart;
        }
    }
}
