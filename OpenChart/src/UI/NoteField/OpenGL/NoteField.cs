using OpenChart.Charting.Properties;
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
        Key[] keys;

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

            keys = new Key[NoteFieldSettings.Chart.KeyCount.Value];

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = new Key(NoteFieldSettings, i);
            }
        }

        private void onDraw(object o, Gtk.DrawnArgs e)
        {
            var ctx = e.Cr;
            var viewRect = ctx.ClipExtents();

            clear(ctx);

            // Center the notefield on the X-axis and scroll it on the Y-axis.
            ctx.Translate((viewRect.Width - NoteFieldSettings.NoteFieldWidth) / 2, NoteFieldSettings.Y);

            var drawContext = newDrawingContext(ctx);
            beatLines.Draw(drawContext);

            for (var i = 0; i < keys.Length; i++)
            {
                ctx.Translate(NoteFieldSettings.KeyWidth, 0);
                keys[i].Draw(drawContext);
            }
        }

        private void clear(Cairo.Context ctx)
        {
            ctx.SetSourceRGB(0.07, 0.07, 0.07);
            ctx.Rectangle(ctx.ClipExtents());
            ctx.Fill();
        }

        private DrawingContext newDrawingContext(Cairo.Context ctx)
        {
            var viewRect = ctx.ClipExtents();
            var pps = (double)NoteFieldSettings.ScaledPixelsPerSecond;
            var topTime = viewRect.Y / pps;
            var bottomTime = (viewRect.Height + viewRect.Y) / pps;

            if (topTime < 0)
                topTime = 0;

            if (bottomTime < 0)
                bottomTime = 0;

            var top = new BeatTime(NoteFieldSettings.Chart.BPMList.Time.TimeToBeat(topTime), topTime);
            var bottom = new BeatTime(NoteFieldSettings.Chart.BPMList.Time.TimeToBeat(bottomTime), bottomTime);

            return new DrawingContext(ctx, top, bottom);
        }
    }
}
