using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// Represents a key index. The key index refers to the column on a note field.
    /// </summary>
    public class KeyIndex
    {
        int _value;

        /// <summary>
        /// The key index. Cannot be negative.
        /// </summary>
        public int Value
        {
            get => _value;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Key index cannot be negative.");
                }

                if (_value != value)
                {
                    _value = value;
                    OnKeyChanged();
                }
            }
        }

        public static implicit operator KeyIndex(int value) => new KeyIndex(value);

        /// <summary>
        /// An event fired when the key index changes.
        /// </summary>
        public event EventHandler KeyChanged;

        /// <summary>
        /// Creates a new Key instance.
        /// </summary>
        public KeyIndex(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Returns true if both keys are the same.
        /// </summary>
        public override bool Equals(object obj)
        {
            var key = obj as KeyIndex;

            if (key == null)
            {
                return false;
            }

            return key.Value == Value;
        }

        /// <summary>
        /// Returns the object's hash code.
        /// </summary>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        protected virtual void OnKeyChanged()
        {
            var handler = KeyChanged;
            handler?.Invoke(this, null);
        }
    }
}