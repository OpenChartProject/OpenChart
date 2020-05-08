using System;

namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// Represents a time, in seconds.
    /// </summary>
    public class Time : IComparable
    {
        double _value;

        /// <summary>
        /// The time (in seconds).
        /// </summary>
        public double Value
        {
            get => _value;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Time cannot be negative.");

                _value = value;
            }
        }

        public static implicit operator Time(double value) => new Time(value);
        public static implicit operator Time(int value) => new Time(value);

        /// <summary>
        /// Creates a new Time instance.
        /// </summary>
        public Time(double value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var time = obj as Time;

            if (time == null)
                return false;

            return Value == time.Value;
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

            var time = o as Time;

            if (time == null)
                throw new ArgumentException("Object is not a Time instance.");

            return Value.CompareTo(time.Value);
        }
    }
}
