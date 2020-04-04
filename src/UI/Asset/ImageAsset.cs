using Gdk;
using System.IO;

namespace OpenChart.UI.Asset
{
    /// <summary>
    /// Represents an image asset.
    /// </summary>
    public class Image
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

        public Image(string path)
        {
            Data = File.ReadAllBytes(path);
            Path = path;
            Pixbuf = new Pixbuf(Data);
        }
    }
}