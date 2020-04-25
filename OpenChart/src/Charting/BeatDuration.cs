using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a beat duration.
    /// </summary>
    public class BeatDuration : IChangeNotifier
    {
        double _value;

        /// <summary>
        /// The duration, in beats. Must be greater than zero.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Beat duration must be greater than zero.");
                else if (_value != value)
                {
                    _value = value;
                    OnChanged();
                }
            }
        }

        public static implicit operator BeatDuration(double value) => new BeatDuration(value);
        public static implicit operator BeatDuration(int value) => new BeatDuration(value);

        /// <summary>
        /// An event fired when the beat value changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Creates a new BeatDuration instance.
        /// </summary>
        public BeatDuration(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var beat = obj as BeatDuration;

            if (beat == null)
                return false;

            return Value == beat.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Compares the value with another object of the same type.
        /// </summary>
        public int CompareTo(object o)
        {
            if (o == null)
                return 1;

            var beatDuration = o as BeatDuration;

            if (beatDuration != null)
                return beatDuration.Value.CompareTo(Value);
            else
                throw new ArgumentException("Object is not a BeatDuration instance.");
        }

        protected virtual void OnChanged()
        {
            var handler = Changed;
            handler?.Invoke(this, null);
        }
    }
}
