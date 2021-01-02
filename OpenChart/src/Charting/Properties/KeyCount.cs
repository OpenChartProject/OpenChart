using System;

namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// Represents a key count. The key count refers to how many keys a chart has.
    /// </summary>
    public class KeyCount : IChangeNotifier
    {
        int _value;

        /// <summary>
        /// The key count. Cannot be negative.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("Key count must be greater than zero.");
                else if (_value != value)
                {
                    _value = value;
                    onChanged();
                }
            }
        }

        public static implicit operator KeyCount(int value) => new KeyCount(value);

        /// <summary>
        /// An event fired when the key count changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Creates a new KeyCount instance.
        /// </summary>
        public KeyCount(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var keyCount = obj as KeyCount;

            if (keyCount == null)
                return false;

            return Value == keyCount.Value;
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
