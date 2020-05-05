using Cairo;
using Gtk;
using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    public class HoldNoteBody : DrawingArea
    {
        ImageAsset image;
        Surface surface;
        SurfacePattern pattern;

        public HoldNoteBody(ImageAsset image)
        {
            this.image = image;
            surface = Gdk.CairoHelper.SurfaceCreateFromPixbuf(image.Pixbuf, 0, null);
            pattern = new SurfacePattern(surface);
            pattern.Extend = Extend.Repeat;
        }

        protected override bool OnDrawn(Context cr)
        {
            // cr.Save();
            cr.SetSource(pattern);
            cr.Rectangle(0, 0, AllocatedWidth, AllocatedHeight);
            // cr.Restore();

            return true;
        }
    }
}
