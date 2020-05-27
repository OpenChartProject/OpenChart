using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.NoteSkins
{
    /// <summary>
    /// A noteskin contains the image assets that are used in the notefield to display the chart.
    /// </summary>
    public class NoteSkin
    {
        /// <summary>
        /// The name of the noteskin. This is the skin's folder name.
        /// <seealso cref="ApplicationData.NoteSkinFolder"/>
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// A list of skins for various keymodes. Noteskins support separate skins for 4k, 7k, etc.
        /// </summary>
        public List<KeyModeSkin> KeyModes;

        /// <summary>
        /// Creates a new noteskin instance.
        /// </summary>
        /// <param name="name">The name of the noteskin.</param>
        public NoteSkin(string name)
        {
            Name = name;
            KeyModes = new List<KeyModeSkin>();
        }

        /// <summary>
        /// Add a noteskin for a particular keymode.
        /// </summary>
        /// <param name="keyMode">The keymode skin.</param>
        public void AddKeyModeSkin(KeyModeSkin keyMode)
        {
            // Check if there is already a skin loaded with the same key count
            if (KeyModes.Exists(km => km.KeyCount.Value == keyMode.KeyCount.Value))
                throw new ArgumentException($"Cannot add duplicate keymode (KeyCount={keyMode.KeyCount}) to noteskin '{Name}'.");

            KeyModes.Add(keyMode);
        }

        /// <summary>
        /// Gets a skin for a particular keymode.
        /// </summary>
        /// <param name="keyCount">The keycount for the keymode to get the skin of.</param>
        /// <returns>The keymode's noteskin, or null if none is set.</returns>
        public KeyModeSkin GetKeyModeSkin(KeyCount keyCount)
        {
            return KeyModes.Find(km => km.KeyCount.Value == keyCount.Value);
        }
    }
}
