using System;

namespace OpenChart.NoteSkins
{
    /// <summary>
    /// Contains the images that are displayed for a particular keymode.
    /// </summary>
    public class KeyModeSkin
    {
        int _keyCount;

        /// <summary>
        /// The number of keys for this keymode.
        /// </summary>
        public int KeyCount
        {
            get => _keyCount;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Key count must be greater than zero.");

                _keyCount = value;
            }
        }

        /// <summary>
        /// The images for each individual key. The leftmost key is at index `0` and the
        /// rightmost key is at `KeyCount - 1`.
        /// </summary>
        public NoteSkinKey[] Keys { get; private set; }

        /// <summary>
        /// Creates a new noteskin instance.
        /// </summary>
        /// <param name="keyCount">The number of keys (columns) this noteskin supports.</param>
        public KeyModeSkin(int keyCount)
        {
            KeyCount = keyCount;
            Keys = new NoteSkinKey[keyCount];
        }

        /// <summary>
        /// Scales all of the key images to fit in the provided width.
        /// </summary>
        /// <param name="width">The new width, in pixels.</param>
        public void ScaleToNoteFieldKeyWidth(int width)
        {
            foreach (var key in Keys)
            {
                key.HoldNote?.ScaleToWidth(width);
                key.HoldNoteBody?.ScaleToWidth(width);
                key.Receptor?.ScaleToWidth(width);
                key.TapNote?.ScaleToWidth(width);
            }
        }

        /// <summary>
        /// Sets the key images to use for a specific key.
        /// </summary>
        /// <param name="key">The key index, from `0` to `KeyCount - 1`.</param>
        /// <param name="keyImage">The noteskin images for the key.</param>
        public void Set(int key, NoteSkinKey keyImage)
        {
            if (key < 0 || key >= KeyCount)
                throw new ArgumentException($"Key must be between 0 and {KeyCount - 1}.");

            Keys[key] = keyImage;
        }
    }
}
