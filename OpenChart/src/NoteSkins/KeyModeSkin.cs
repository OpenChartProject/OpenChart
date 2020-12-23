using OpenChart.Charting.Properties;
using OpenChart.UI;
using System;

namespace OpenChart.NoteSkins
{
    /// <summary>
    /// Contains the images that are displayed for a particular keymode.
    /// </summary>
    public class KeyModeSkin
    {
        public KeyCount KeyCount;

        /// <summary>
        /// The image data for each key. You probably want <see cref="ScaledKeys" />.
        /// </summary>
        public NoteSkinKey[] Keys { get; private set; }

        /// <summary>
        /// The scaled image for each key.
        /// </summary>
        public NoteSkinKey[] ScaledKeys { get; private set; }

        /// <summary>
        /// Creates a new noteskin instance.
        /// </summary>
        /// <param name="keyCount">The number of keys (columns) this noteskin supports.</param>
        public KeyModeSkin(KeyCount keyCount)
        {
            KeyCount = keyCount;
            Keys = new NoteSkinKey[KeyCount.Value];
            ScaledKeys = new NoteSkinKey[KeyCount.Value];
        }

        /// <summary>
        /// Scales all of the key images to fit in the provided width.
        /// </summary>
        /// <param name="width">The new width, in pixels.</param>
        public void ScaleToNoteFieldKeyWidth(int width)
        {
            for (var i = 0; i < Keys.Length; i++)
            {
                // Dispose of the old scaled images.
                ScaledKeys[i]?.Dispose();
                ScaledKeys[i] = new NoteSkinKey();

                ScaledKeys[i].HoldNote = Keys[i].HoldNote.ScaleTo(width, SurfaceScaleType.Width);
                ScaledKeys[i].TapNote = Keys[i].TapNote.ScaleTo(width, SurfaceScaleType.Width);
                // ScaledKeys[i].Receptor = Keys[i].Receptor.ScaleTo(width, SurfaceScaleType.Width);

                var scaledBody = Keys[i].HoldNoteBody.Surface.ScaleTo(width, SurfaceScaleType.Width);
                ScaledKeys[i].HoldNoteBody = new SurfacePattern(scaledBody);
            }
        }

        /// <summary>
        /// Sets the key images to use for a specific key.
        /// </summary>
        /// <param name="key">The key index, from `0` to `KeyCount - 1`.</param>
        /// <param name="keyImage">The noteskin images for the key.</param>
        public void Set(KeyIndex key, NoteSkinKey keyImage)
        {
            if (key.Value >= KeyCount.Value)
                throw new ArgumentException($"Key index is out of range.");

            Keys[key.Value] = keyImage;
        }
    }
}
