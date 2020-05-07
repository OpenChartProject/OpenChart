using Cairo;
using Gdk;
using Gtk;
using OpenChart.UI.Assets;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// Widget for the hold note body. Renders a repeating image for the length of the hold note.
    /// </summary>
    public class HoldNoteBody : DrawingArea
    {
        ImageAsset image;
        static Pixbuf surfacePixbuf;
        static Surface surface;
        static SurfacePattern pattern;

        /// <summary>
        /// Creates a new HoldNoteBody instance.
        /// </summary>
        /// <param name="image">The image of the hold note body.</param>
        public HoldNoteBody(ImageAsset image)
        {
            this.image = image;

            // Create a new surface for the image so we can repeat it.
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
