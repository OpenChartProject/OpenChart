using OpenChart.Charting.Properties;
using System;

namespace OpenChart.UI.NoteField
{
    public class NoteField
    {
        /// <summary>
        /// The settings for the note field.
        /// </summary>
        public NoteFieldSettings NoteFieldSettings { get; private set; }

        // The amount of pixels to move the notefield when the user scrolls with their mouse.
        const int scrollSpeed = 50;

        // How far the notefield can be scrolled before it will stop scrolling. This stops the user
        // from scrolling past the beginning of the chart.
        const int scrollStop = 100;

        BeatLines beatLines;
        Key[] keys;

        public NoteField(NoteFieldSettings noteFieldSettings, BeatLineSettings beatLineSettings)
        {
            NoteFieldSettings = noteFieldSettings;

            beatLines = new BeatLines(NoteFieldSettings, beatLineSettings);
            canvas = new Gtk.Layout(null, null);
            canvas.Drawn += onDraw;
            canvas.ScrollEvent += onScroll;

            keys = new Key[NoteFieldSettings.Chart.KeyCount.Value];

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = new Key(NoteFieldSettings, i);
            }
        }

        private void clear(Cairo.Context ctx)
        {
            ctx.SetSourceRGB(0.07, 0.07, 0.07);
            ctx.Paint();
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

            ctx.Save();

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i].Draw(drawContext);
                ctx.Translate(NoteFieldSettings.KeyWidth, 0);
            }

            ctx.Restore();
        }

        private void onScroll(object o, Gtk.ScrollEventArgs e)
        {
            var newY = NoteFieldSettings.Y - (int)Math.Round(e.Event.DeltaY * scrollSpeed);

            if (newY > scrollStop)
                newY = scrollStop;

            if (newY != NoteFieldSettings.Y)
            {
                NoteFieldSettings.Y = newY;
                canvas.QueueDraw();
            }
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
