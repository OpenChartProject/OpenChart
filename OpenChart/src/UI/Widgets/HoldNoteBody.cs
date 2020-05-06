using Cairo;
using Gdk;
using Gtk;
using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    public class HoldNoteBody : DrawingArea
    {
        ImageAsset image;
        static Pixbuf surfacePixbuf;
        static Surface surface;
        static SurfacePattern pattern;

        public HoldNoteBody(ImageAsset image)
        {
            this.image = image;

            if (surfacePixbuf != image.Pixbuf)
            {
                surfacePixbuf = image.Pixbuf;
                surface = Gdk.CairoHelper.SurfaceCreateFromPixbuf(surfacePixbuf, 0, null);
                pattern = new SurfacePattern(surface);
                pattern.Extend = Extend.Repeat;
            }
        }

        protected override bool OnDrawn(Context cr)
        {
            cr.SetSource(pattern);
            cr.Rectangle(0, 0, AllocatedWidth, AllocatedHeight);
            cr.Fill();

            return true;
        }
    }
}
