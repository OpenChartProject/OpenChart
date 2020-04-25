using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a beat.
    /// </summary>
    public class Beat : IChangeNotifier
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
                    throw new ArgumentOutOfRangeException("Beat cannot be negative.");

                if (_value != value)
                {
                    _value = value;
                    OnChanged();
                }
            }
        }

        public static implicit operator Beat(double value) => new Beat(value);
        public static implicit operator Beat(int value) => new Beat(value);

        /// <summary>
        /// An event fired when the beat value changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Creates a new Beat instance.
        /// </summary>
        public Beat(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var beat = obj as Beat;

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

            var beat = o as Beat;

            if (beat != null)
                return beat.Value.CompareTo(Value);
            else
                throw new ArgumentException("Object is not a Beat instance.");
        }

        protected virtual void OnChanged()
        {
            var handler = Changed;
            handler?.Invoke(this, null);
        }
    }
}
