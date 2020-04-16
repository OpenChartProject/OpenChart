using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a beat.
    /// </summary>
    public class Beat : IComparable
    {
        double _value;

        /// <summary>
        /// The beat value. Cannot be negative.
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Beat cannot be negative.");
                }

                if (_value != value)
                {
                    _value = value;
                    OnBeatChanged();
                }
            }
        }

        public static implicit operator double(Beat b) => b.Value;
        public static implicit operator Beat(double value) => new Beat(value);
        public static implicit operator Beat(int value) => new Beat(value);

        /// <summary>
        /// An event fired when the beat value changes.
        /// </summary>
        public event EventHandler BeatChanged;

        /// <summary>
        /// Creates a new Beat instance.
        /// </summary>
        public Beat(double value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns true if both beat objects occur on the same beat.
        /// </summary>
        public override bool Equals(object obj)
        {
            var beat = obj as Beat;

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

            var beat = o as Beat;

            if (beat != null)
            {
                return beat.Value.CompareTo(Value);
            }
            else
            {
                throw new ArgumentException("Object is not a Beat instance.");
            }
        }

        protected virtual void OnBeatChanged()
        {
            var handler = BeatChanged;
            handler?.Invoke(this, null);
        }
    }
}