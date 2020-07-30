using Cairo;

namespace OpenChart.UI.Assets
{
    /// <summary>
    /// Represents an image pattern. This is useful for when you need to tile an image.
    /// TODO: Remove
    /// </summary>
    public class ImagePattern
    {
        /// <summary>
        /// The image asset this pattern is displaying.
        /// </summary>
        public ImageAsset ImageAsset { get; private set; }

        /// <summary>
        /// The surface pattern. <seealso cref="Cairo.Context.SetSource(Surface)" />
        /// </summary>
        public SurfacePattern Pattern { get; private set; }

        /// <summary>
        /// The surface created from the image.
        /// </summary>
        public Surface Surface { get; private set; }

        /// <summary>
        /// Creates a new ImagePattern instance.
        /// </summary>
        /// <param name="image">The image asset to use.</param>
        /// <param name="extendType">How the pattern should behave. Tile is the default.</param>
        public ImagePattern(ImageAsset image, Extend extendType = Extend.Repeat)
        {
            ImageAsset = image;

            Surface = Gdk.CairoHelper.SurfaceCreateFromPixbuf(ImageAsset.Pixbuf, 0, null);
            Pattern = new SurfacePattern(Surface);
            Pattern.Extend = extendType;
        }
    }
}
