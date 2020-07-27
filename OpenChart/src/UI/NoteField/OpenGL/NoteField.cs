using Serilog;

namespace OpenChart.UI.NoteField.OpenGL
{
    public class NoteField : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        BeatLines beatLines;
        Gtk.DrawingArea canvas;

        public Gtk.Widget GetWidget() => canvas;

        public NoteField(NoteFieldSettings noteFieldSettings, BeatLineSettings beatLineSettings)
        {
            NoteFieldSettings = noteFieldSettings;

            beatLines = new BeatLines(NoteFieldSettings, beatLineSettings);
            canvas = new Gtk.DrawingArea();
            canvas.Drawn += onDraw;
        }

        private void onDraw(object o, Gtk.DrawnArgs e)
        {
            var ctx = e.Cr;

            // Clear the drawing area.
            ctx.SetSourceRGB(0, 0, 0);
            ctx.Rectangle(ctx.ClipExtents());
            ctx.Fill();

            beatLines.Draw(ctx);
        }
    }
}
