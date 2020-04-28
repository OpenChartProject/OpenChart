using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// A widget that uses an ImageAsset for the image data.
    /// </summary>
    public class Image : Gtk.Image
    {
        ImageAsset _imageAsset;

        /// <summary>
        /// The ImageAsset this widget is displaying.
        /// </summary>
        public ImageAsset ImageAsset
        {
            get => _imageAsset;
            set
            {
                _imageAsset = value;
                Pixbuf = _imageAsset?.Pixbuf;
            }
        }

        public Image(ImageAsset imageAsset)
        {
            ImageAsset = imageAsset;
        }
    }
}
