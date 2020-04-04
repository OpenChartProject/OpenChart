using Gdk;
using System.IO;

namespace charter.UI.Asset
{
    /// <summary>
    /// Represents an image asset.
    /// </summary>
    public class Image
    {
        public readonly byte[] Data;
        public readonly string Path;
        public readonly Pixbuf PixBuf;

        public Image(string path)
        {
            Data = File.ReadAllBytes(path);
            Path = path;
            PixBuf = new Pixbuf(Data);
        }
    }
}