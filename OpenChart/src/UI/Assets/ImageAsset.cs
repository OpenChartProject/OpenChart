using Gdk;
using System;
using System.IO;

namespace OpenChart.UI.Assets
{
    /// <summary>
    /// Represents an image asset.
    /// </summary>
    public class ImageAsset
    {
        /// <summary>
        /// The raw image data.
        /// </summary>
        public readonly byte[] Data;

        /// <summary>
        /// The absolute path to the image file. The path is null if the constructor is
        /// passed the image data directly.
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// The pixel buffer used by Gdk.
        /// </summary>
        public Pixbuf Pixbuf { get; private set; }

        /// <summary>
        /// Creates a new ImageAsset instance.
        /// </summary>
        /// <param name="path">The path to the image file.</param>
        public ImageAsset(string path)
        {
            Data = File.ReadAllBytes(path);
            Path = path;
            Pixbuf = new Pixbuf(Data);
        }

        /// <summary>
        /// Creates a new ImageAsset instance.
        /// </summary>
        /// <param name="data">The raw image data.</param>
        public ImageAsset(byte[] data)
        {
            Data = data;
            Path = null;
            Pixbuf = new Pixbuf(Data);
        }

        /// <summary>
        /// Resizes the image to the provided width and height.
        /// </summary>
        /// <param name="width">The width to resize to (in pixels).</param>
        /// <param name="height">The height to resize to (in pixels).</param>
        public void Resize(int width, int height)
        {
            // Not using any scaling functions here because if we shrink and then enlarge
            // the pixbuf we'll lose some image clarity.
            Pixbuf = new Pixbuf(Data, width, height);
        }

        /// <summary>
        /// Scales the image width to the new width while keeping the aspect ratio the same.
        /// </summary>
        /// <param name="width">The width to scale the image to (in pixels).</param>
        public void ScaleToWidth(int width)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("Width must be greater than zero.");
            else if (width == Pixbuf.Width)
                return;

            // Maintain the aspect ratio by scaling the height by the same %.
            var scalePercent = (double)width / Pixbuf.Width;
            var newHeight = (int)Math.Round(Pixbuf.Height * scalePercent);

            Resize(width, newHeight);
        }
    }
}
