using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a key count. The key count refers to how many keys a chart has.
    /// </summary>
    public class KeyCount : IComparable
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
                {
                    throw new ArgumentOutOfRangeException("Key count must be greater than zero.");
                }

                if (_value != value)
                {
                    _value = value;
                    OnKeyCountChanged();
                }
            }
        }

        public static implicit operator KeyCount(int value) => new KeyCount(value);

        /// <summary>
        /// An event fired when the key count changes.
        /// </summary>
        public event EventHandler KeyCountChanged;

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
            {
                return false;
            }

            return keyCount.Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        protected virtual void OnKeyCountChanged()
        {
            var handler = KeyCountChanged;
            handler?.Invoke(this, null);
        }
    }
}