using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a BPM (beats per minute) change in a chart.
    /// </summary>
    public class BPM : IBeatObject, IChangeNotifier
    {
        double _value;

        /// <summary>
        /// Beats per minute. Most songs are somewhere in the range of 120 to 200.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("BPM must be greater than zero.");
                }

                if (_value != value)
                {
                    _value = value;
                    OnChanged();
                }
            }
        }

        /// <summary>
        /// The beat where the BPM change occurs.
        /// </summary>
        public Beat Beat { get; private set; }

        /// <summary>
        /// An event fired when the BPM changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Creates a new BPM instance.
        /// </summary>
        /// <param name="value">The beats per minute.</param>
        /// <param name="beat">The beat this BPM change occurs on.</param>
        public BPM(double value, double beat)
        {
            Beat = new Beat(beat);
            Value = value;

            Beat.Changed += delegate { OnChanged(); };
        }

        public override bool Equals(object obj)
        {
            var bpm = obj as BPM;

            if (bpm == null)
            {
                return false;
            }

            return Beat == bpm.Beat && Value == bpm.Value;
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Beat, Value).GetHashCode();
        }

        protected virtual void OnChanged()
        {
            var handler = Changed;
            handler?.Invoke(this, null);
        }
    }
}