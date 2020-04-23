using System;

namespace OpenChart.Charting.Properties
{
    /// <summary>
    /// Represents a key index. The key index refers to the column on a note field.
    /// </summary>
    public class KeyIndex : IChangeNotifier
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
                    OnChanged();
                }
            }
        }

        public static implicit operator KeyIndex(int value) => new KeyIndex(value);

        /// <summary>
        /// An event fired when the key index changes.
        /// </summary>
        public event EventHandler Changed;

        /// <summary>
        /// Creates a new Key instance.
        /// </summary>
        public KeyIndex(int value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var key = obj as KeyIndex;

            if (key == null)
            {
                return false;
            }

            return Value == key.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        protected virtual void OnChanged()
        {
            var handler = Changed;
            handler?.Invoke(this, null);
        }
    }
}
