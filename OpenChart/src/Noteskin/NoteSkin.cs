using System;
using System.Collections.Generic;

namespace OpenChart.NoteSkin
{
    /// <summary>
    /// A noteskin contains the image assets that are used in the notefield to display the chart.
    /// </summary>
    public class NoteSkin
    {
        /// <summary>
        /// The name of the noteskin. This is the skin's folder name.
        /// <seealso cref="App.NoteSkinFolder"/>
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// A list of skins for various keymodes. Noteskins support separate skins for 4k, 7k, etc.
        /// </summary>
        List<KeyModeSkin> keyModes;

        public NoteSkin(string name)
        {
            Name = name;
            keyModes = new List<KeyModeSkin>();
        }

        /// <summary>
        /// Add a noteskin for a particular keymode.
        /// </summary>
        /// <param name="keyMode">The keymode skin.</param>
        public void AddKeyModeSkin(KeyModeSkin keyMode)
        {
            // Check if there is already a skin loaded with the same key count
            if (keyModes.Exists(km => km.KeyCount == keyMode.KeyCount))
            {
                throw new ArgumentException($"Cannot add duplicate keymode (KeyCount={keyMode.KeyCount}) to noteskin '{Name}'.");
            }

            keyModes.Add(keyMode);
        }

        /// <summary>
        /// Gets a skin for a particular keymode.
        /// </summary>
        /// <param name="keyCount">The keycount for the keymode to get the skin of.</param>
        /// <returns>The keymode's noteskin, or null if none is set.</returns>
        public KeyModeSkin GetKeyModeSkin(int keyCount)
        {
            if (keyCount <= 0)
            {
                throw new ArgumentException("Key count must be greater than zero.");
            }

            return keyModes.Find(km => km.KeyCount == keyCount);
        }
    }
}