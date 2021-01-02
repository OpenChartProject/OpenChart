using System;

namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// Represents a beat division.
    /// </summary>
    public class BeatDivision
    {
        public const int BEAT_DIVISION_MAX = 192;
        public const double ROUNDING_ERROR = 0.001;

        int _value;

        /// <summary>
        /// The beat division. Must be between 1 and BEAT_DIVISION_MAX.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                if (value < 1 || value > BEAT_DIVISION_MAX)
                    throw new ArgumentOutOfRangeException("Beat division is out of range.");

                _value = value;
            }
        }

        public double DivisionLength => 1.0 / Value;

        public static implicit operator BeatDivision(int value) => new BeatDivision(value);

        /// <summary>
        /// Creates a new BeatDivision instance.
        /// </summary>
        public BeatDivision(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns true if the beat is evenly divisible by the beat division. This allows for a
        /// slight amount of rounding error.
        /// </summary>
        public bool IsAligned(Beat beat)
        {
            // Get the decimal part of the beat.
            // e.g. 4.67 -> 0.67
            var dec = beat.Value - Math.Truncate(beat.Value);

            // Get the number of divisions that fit.
            // e.g. 0.67 / 0.25 = 2.68
            var divCount = dec / DivisionLength;

            // Calculate how close the division count is to the nearest whole number.
            // e.g. abs(2.68 - 3) = 0.32
            var diff = Math.Abs(divCount - Math.Round(divCount));

            return diff <= ROUNDING_ERROR;
        }

        /// <summary>
        /// Given a beat, calculates the next division that occurs and returns it.
        ///
        /// For example, if the beat division is 1/4 = 0.25, and the provided beat is 1.333, the
        /// next beat division would occur at 1.5.
        /// </summary>
        public Beat NextDivisionFromBeat(Beat beat)
        {
            return Math.Ceiling(beat.Value / DivisionLength) * DivisionLength;
        }

        /// <summary>
        /// Given a beat, calculates the previous division that occurs and returns it. See
        /// <see cref="NextDivisionFromBeat"/> for more info.
        /// </summary>
        public Beat PrevDivisionFromBeat(Beat beat)
        {
            return Math.Floor(beat.Value / DivisionLength) * DivisionLength;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
