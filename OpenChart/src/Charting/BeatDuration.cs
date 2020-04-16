using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a beat duration.
    /// </summary>
    public class BeatDuration : IComparable
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
                {
                    throw new ArgumentOutOfRangeException("Beat duration must be greater than zero.");
                }

                if (_value != value)
                {
                    _value = value;
                    OnBeatDurationChanged();
                }
            }
        }

        public static implicit operator double(BeatDuration bd) => bd.Value;
        public static implicit operator BeatDuration(double value) => new BeatDuration(value);
        public static implicit operator BeatDuration(int value) => new BeatDuration(value);

        /// <summary>
        /// An event fired when the beat value changes.
        /// </summary>
        public event EventHandler BeatDurationChanged;

        /// <summary>
        /// Creates a new BeatDuration instance.
        /// </summary>
        public BeatDuration(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns true if both beat objects occur on the same beat.
        /// </summary>
        public override bool Equals(object obj)
        {
            var beat = obj as BeatDuration;

            if (beat == null)
            {
                return false;
            }

            return Value == beat.Value;
        }

        /// <summary>
        /// Returns the object's hash code.
        /// </summary>
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
            {
                return 1;
            }

            var beatDuration = o as BeatDuration;

            if (beatDuration != null)
            {
                return beatDuration.Value.CompareTo(Value);
            }
            else
            {
                throw new ArgumentException("Object is not a BeatDuration instance.");
            }
        }

        protected virtual void OnBeatDurationChanged()
        {
            var handler = BeatDurationChanged;
            handler?.Invoke(this, null);
        }
    }
}