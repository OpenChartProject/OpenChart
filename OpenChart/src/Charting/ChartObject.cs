using System;

namespace OpenChart.Charting
{
    public abstract class ChartObject : Timing
    {
        int _key;

        /// <summary>
        /// The key index this object occurs on. The first key starts at zero.
        /// </summary>
        public int Key
        {
            get => _key;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Key cannot be less than 0.");
                }

                _key = value;
            }
        }

        public ChartObject(int key, double beat) : base(beat)
        {
            Key = key;
        }
    }
}