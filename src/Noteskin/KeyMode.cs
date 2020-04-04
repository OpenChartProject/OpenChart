using System;

namespace charter.NoteSkin
{
    /// <summary>
    /// Contains the images that are displayed for a particular keymode.
    /// </summary>
    public class KeyMode
    {
        int _keyCount;

        public int KeyCount
        {
            get => _keyCount;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Key count must be greater than zero.");
                }

                _keyCount = value;
            }
        }

        public NoteSkinKey[] Images { get; private set; }

        public KeyMode(int keyCount)
        {
            KeyCount = keyCount;
            Images = new NoteSkinKey[keyCount];
        }

        public void Set(int column, NoteSkinKey keyImage)
        {
            if (column < 0 || column >= KeyCount)
            {
                throw new ArgumentException($"Column must be between 0 and {KeyCount}.");
            }

            Images[column] = keyImage;
        }
    }
}