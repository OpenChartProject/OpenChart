using System;

namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// Represents a beat.
    /// </summary>
    public class Beat : IChangeNotifier, IComparable
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
                else if (_value != value)
                {
                    _value = value;
                    onChanged();
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

        /// <summary>
        /// Returns true if this beat is at the start of a measure. A beat is considered to be
        /// at the start of a measure if it's a whole number and evenly divisble by 4.
        /// </summary>
        public bool IsStartOfMeasure()
        {
            var floor = (int)Math.Floor(Value);

            // Not a whole number.
            if (floor != Value)
                return false;

            return floor % 4 == 0;
        }

        public int CompareTo(object o)
        {
            if (o == null)
                return 1;

            var beat = o as Beat;

            if (beat == null)
                throw new ArgumentException("Object is not a Beat instance.");

            return Value.CompareTo(beat.Value);
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

        public override string ToString()
        {
            return Value.ToString();
        }

        protected virtual void onChanged()
        {
            var handler = Changed;
            handler?.Invoke(this, null);
        }
    }
}
