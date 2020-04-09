using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// The base class for any object that the player interacts with in a chart.
    /// </summary>
    public abstract class ChartObject : Timing
    {
        int _key;

        /// <summary>
        /// The key index this object occurs on.
        /// </summary>
        public int Key
        {
            get => _key;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Key cannot be less than 1.");
                }

                _key = value;
            }
        }

        public ChartObject(int key, double beat) : base(beat)
        {
            Key = key;
        }

        /// <summary>
        /// Checks if both chart objects occur on the same beat and key.
        /// </summary>
        public override bool Equals(object obj)
        {
            var chartObj = obj as ChartObject;

            if (chartObj == null)
            {
                return false;
            }

            return Beat == chartObj.Beat && Key == chartObj.Key;
        }

        /// <summary>
        /// Returns the object's hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return Tuple.Create(Beat, Key).GetHashCode();
        }
    }
}