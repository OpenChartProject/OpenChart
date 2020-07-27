using Serilog;
using System;

namespace OpenChart.UI.NoteField.OpenGL
{
    public class NoteField : IWidget
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        BeatLines beatLines;
        Gtk.Layout canvas;

        public Gtk.Widget GetWidget() => canvas;

        public NoteField(NoteFieldSettings noteFieldSettings, BeatLineSettings beatLineSettings)
        {
            NoteFieldSettings = noteFieldSettings;

            beatLines = new BeatLines(NoteFieldSettings, beatLineSettings);
            canvas = new Gtk.Layout(null, null);
            canvas.Drawn += onDraw;
            canvas.ScrollEvent += (o, e) =>
            {
                NoteFieldSettings.X -= (int)Math.Round(e.Event.DeltaX * 50);
                NoteFieldSettings.Y -= (int)Math.Round(e.Event.DeltaY * 50);
                canvas.QueueDraw();
            };
        }

        private void onDraw(object o, Gtk.DrawnArgs e)
        {
            var ctx = e.Cr;
            var viewRect = ctx.ClipExtents();

            // Clear the drawing area.
            ctx.SetSourceRGB(0.07, 0.07, 0.07);
            ctx.Rectangle(viewRect);
            ctx.Fill();

            ctx.Translate((viewRect.Width - NoteFieldSettings.NoteFieldWidth) / 2, NoteFieldSettings.Y);

            beatLines.Draw(ctx);
        }
    }
}
