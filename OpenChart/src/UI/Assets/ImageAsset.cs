using Gdk;
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
        /// The absolute path to the image file.
        /// </summary>
        public readonly string Path;

        /// <summary>
        /// A reusable Pixbuf instance.
        /// </summary>
        public readonly Pixbuf Pixbuf;

        public ImageAsset(string path)
        {
            Data = File.ReadAllBytes(path);
            Path = path;
            Pixbuf = new Pixbuf(Data);
        }
    }
}