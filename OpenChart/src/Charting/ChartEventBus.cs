using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Event args used for BPM related events.
    /// </summary>
    public class BPMEventArgs : EventArgs
    {
        public readonly BPM BPM;

        public BPMEventArgs(BPM bpm)
        {
            BPM = bpm;
        }
    }

    /// <summary>
    /// Event args used for chart object related events.
    /// </summary>
    public class ObjectEventArgs : EventArgs
    {
        public readonly BaseObject Object;

        public ObjectEventArgs(BaseObject obj)
        {
            Object = obj;
        }
    }

    /// <summary>
    /// An event bus that wraps a Chart object. This provides a single source of truth for
    /// events fired by a Chart and its components.
    /// </summary>
    public class ChartEventBus
    {
        /// <summary>
        /// The chart object being watched.
        /// </summary>
        public readonly Chart Chart;

        /// <summary>
        /// Fired when a new BPM is added.
        /// </summary>
        public event EventHandler BPMAdded;

        /// <summary>
        /// Fired when an existing BPM is changed.
        /// </summary>
        public event EventHandler BPMChanged;

        /// <summary>
        /// Fired when an existing BPM is removed.
        /// </summary>
        public event EventHandler BPMRemoved;

        /// <summary>
        /// Fired when a new chart object is added.
        /// </summary>
        public event EventHandler ObjectAdded;

        /// <summary>
        /// Fired when an existing chart object is removed.
        /// </summary>
        public event EventHandler ObjectRemoved;

        public ChartEventBus(Chart chart)
        {
            if (chart == null)
                throw new ArgumentNullException("Chart cannot be null.");

            Chart = chart;

            Chart.BPMs.Added += onBPMAdded;
            Chart.BPMs.Removed += onBPMRemoved;

            // Listen to the object lists for each key.
            foreach (var keyObjects in Chart.Objects)
            {
                keyObjects.Added += onObjectAdded;
                keyObjects.Removed += onObjectRemoved;
            }
        }

        private void onBPMAdded(object o, EventArgs e)
        {
            var incomingArgs = e as ObjectListEventArgs<BPM>;
            var outgoingArgs = new BPMEventArgs(incomingArgs.Object);

            incomingArgs.Object.Changed += onBPMChanged;

            BPMAdded?.Invoke(this, outgoingArgs);
        }

        private void onBPMChanged(object o, EventArgs e)
        {
            var bpm = o as BPM;
            var outgoingArgs = new BPMEventArgs(bpm);

            BPMChanged?.Invoke(this, outgoingArgs);
        }

        private void onBPMRemoved(object o, EventArgs e)
        {
            var incomingArgs = e as ObjectListEventArgs<BPM>;
            var outgoingArgs = new BPMEventArgs(incomingArgs.Object);

            incomingArgs.Object.Changed -= onBPMChanged;

            BPMRemoved?.Invoke(this, outgoingArgs);
        }

        private void onObjectAdded(object o, EventArgs e)
        {
            var incomingArgs = e as ObjectListEventArgs<BaseObject>;
            var outgoingArgs = new ObjectEventArgs(incomingArgs.Object);

            ObjectAdded?.Invoke(this, outgoingArgs);
        }

        private void onObjectRemoved(object o, EventArgs e)
        {
            var incomingArgs = e as ObjectListEventArgs<BaseObject>;
            var outgoingArgs = new ObjectEventArgs(incomingArgs.Object);

            ObjectRemoved?.Invoke(this, outgoingArgs);
        }
    }
}
