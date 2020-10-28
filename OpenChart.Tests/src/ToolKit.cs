using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
using OpenChart.UI.Assets;
using System;

namespace OpenChart.Tests
{
    public class ToolKit
    {
        /// <summary>
        /// Singleton toolkit instance.
        /// </summary>
        static ToolKit instance;

        /// <summary>
        /// A 1x1 transparent PNG, encoded with base64.
        /// </summary>
        const string encodedBlankImage = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR4nGP6zwAAAgcBApocMXEAAAAASUVORK5CYII=";

        /// <summary>
        /// The raw data for a 1x1 transparent PNG.
        /// </summary>
        public byte[] BlankImageData = Convert.FromBase64String(encodedBlankImage);

        /// <summary>
        /// A new ImageAsset instance which displays a 1x1 transparent image.
        /// </summary>
        public ImageAsset BlankImageAsset => new ImageAsset(BlankImageData);

        /// <summary>
        /// A noteskin that uses blank images, suitable for testing.
        /// </summary>
        public NoteSkin NoteSkin;

        private ToolKit()
        {
            NoteSkin = new NoteSkin("test-skin");
            NoteSkin.AddKeyModeSkin(NewTestSkin(4));
        }

        /// <summary>
        /// Gets the toolkit as a singleton.
        /// </summary>
        public static ToolKit GetInstance()
        {
            if (instance == null)
                instance = new ToolKit();

            return instance;
        }

        /// <summary>
        /// Returns a new KeyModeSkin that uses a blank image for all the key assets.
        /// </summary>
        public KeyModeSkin NewTestSkin(KeyCount keyCount)
        {
            var skin = new KeyModeSkin(keyCount);

            for (var i = 0; i < keyCount.Value; i++)
            {
                skin.Keys[i] = new NoteSkinKey
                {
                    HoldNoteBody = new ImagePattern(BlankImageAsset),
                    HoldNote = BlankImageAsset,
                    Receptor = BlankImageAsset,
                    TapNote = BlankImageAsset,
                };
            }

            return skin;
        }
    }
}
