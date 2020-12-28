using OpenChart.Charting.Properties;
using Serilog;
using System;

namespace OpenChart.UI.NoteField
{
    public class NoteField : IDrawable
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

        Cairo.Color bgColor = new Cairo.Color(0.07, 0.07, 0.07);

        BeatLines beatLines;
        Key[] keys;

        public NoteField(NoteFieldSettings noteFieldSettings, BeatLineSettings beatLineSettings)
        {
            NoteFieldSettings = noteFieldSettings;
            beatLines = new BeatLines(NoteFieldSettings, beatLineSettings);
            keys = new Key[NoteFieldSettings.Chart.KeyCount.Value];

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i] = new Key(NoteFieldSettings, i);
            }
        }

        public void Draw(Cairo.Context _ctx)
        {
            var ctx = newDrawingContext(_ctx);
            doDraw(ctx);
        }

        public void Scroll(double delta)
        {
            ScrollTo(NoteFieldSettings.Y - (int)Math.Round(delta * scrollSpeed));
        }

        public void ScrollTo(int y)
        {
            if (y > scrollStop)
                y = scrollStop;

            NoteFieldSettings.Y = y;
        }

        private void doDraw(DrawingContext ctx)
        {
            var viewRect = ctx.Cairo.FillExtents();

            ctx.Cairo.SetSourceColor(bgColor);
            ctx.Cairo.Paint();

            // Center the notefield on the X-axis and scroll it on the Y-axis.
            ctx.Cairo.Translate((viewRect.Width - NoteFieldSettings.NoteFieldWidth) / 2, NoteFieldSettings.Y);
            beatLines.Draw(ctx);
            ctx.Cairo.Save();

            for (var i = 0; i < keys.Length; i++)
            {
                keys[i].Draw(ctx);
                ctx.Cairo.Translate(NoteFieldSettings.KeyWidth, 0);
            }

            ctx.Cairo.Restore();
        }

        private DrawingContext newDrawingContext(Cairo.Context ctx)
        {
            var yTop = -NoteFieldSettings.Y;
            var viewRect = ctx.FillExtents();
            var pps = (double)NoteFieldSettings.ScaledPixelsPerSecond;
            var topTime = yTop / pps;
            var bottomTime = (yTop + viewRect.Height) / pps;

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
