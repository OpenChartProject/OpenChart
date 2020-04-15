using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    public class NoteWidget : NoteFieldObject
    {
        ImageAsset _image;
        public ImageAsset Image
        {
            get => _image;
            set
            {
                _image = value;
                Pixbuf = _image.Pixbuf;
            }
        }

        public NoteWidget(int key, ImageAsset image) : base(key)
        {
            Image = image;
        }
    }
}