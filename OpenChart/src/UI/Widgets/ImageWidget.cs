using Gtk;
using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// A simple image widget that supports using an ImageAsset object to set the image graphic.
    /// </summary>
    public class ImageWidget : Image
    {
        ImageAsset _image;

        /// <summary>
        /// The image to display. Can be null.
        /// </summary>
        public ImageAsset Image
        {
            get => _image;
            set
            {
                _image = value;
                Pixbuf = Image?.Pixbuf;
            }
        }
    }
}
