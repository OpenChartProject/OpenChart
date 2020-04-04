using System;
using System.Collections.Generic;

namespace charter.NoteSkin
{
    /// <summary>
    /// A noteskin contains the image assets that are used in the notefield to display the chart.
    /// </summary>
    public class NoteSkin
    {
        public readonly string Name;
        List<KeyMode> keyModes;

        public NoteSkin(string name)
        {
            Name = name;
            keyModes = new List<KeyMode>();
        }

        public void AddKeyModeSkin(KeyMode keyMode)
        {
            // Check if there is already a skin loaded with the same key count
            if (keyModes.Exists(km => km.KeyCount == keyMode.KeyCount))
            {
                throw new ArgumentException($"Cannot add duplicate keymode (KeyCount={keyMode.KeyCount}) to noteskin '{Name}'.");
            }

            keyModes.Add(keyMode);
        }

        public KeyMode GetSkin(int keyCount)
        {
            if (keyCount <= 0)
            {
                throw new ArgumentException("Key count must be greater than zero.");
            }

            return keyModes.Find(km => km.KeyCount == keyCount);
        }
    }
}