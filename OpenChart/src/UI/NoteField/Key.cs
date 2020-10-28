using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.UI.NoteField
{
    public class Key : IDrawable
    {
        public KeyIndex Index { get; private set; }

        public NoteFieldSettings NoteFieldSettings { get; private set; }

        public Key(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            Index = index;
            NoteFieldSettings = noteFieldSettings;
        }

        public void Draw(DrawingContext ctx)
        {
            // This controls how much of a vertical margin we are giving ourselves to draw beyond
            // the screen. This fixes an issue where an object close to the edge of the screen may
            // not be drawn correctly.
            var margin = 100;
            var iter = NoteFieldSettings.Chart.Objects[Index.Value].GetEnumerator();

            while (iter.MoveNext())
            {
                var cur = iter.Current;

                if ((cur.Time.Value + margin) < ctx.Top.Time.Value)
                    continue;
                else if ((cur.Time.Value - margin) > ctx.Bottom.Time.Value)
                    break;

                drawObject(ctx, cur);
            }
        }

        private void drawObject(DrawingContext ctx, BaseObject obj)
        {
            var y = NoteFieldSettings.TimeToPosition(obj.Time);

            if (obj is TapNote tapNote)
                drawTapNote(ctx, tapNote, y);
            else if (obj is HoldNote holdNote)
                drawHold(ctx, holdNote, y);
        }

        private void drawTapNote(DrawingContext ctx, TapNote obj, int y)
        {
            var img = NoteFieldSettings.NoteSkin.Keys[Index.Value].TapNote;

            // Reposition the note based on the notefield baseline.
            var offsetY = y - (int)(NoteFieldSettings.BaseLine * img.Pixbuf.Height);

            ctx.Cairo.SetSourceSurface(img.Surface, 0, offsetY);
            ctx.Cairo.Paint();
        }

        private void drawHold(DrawingContext ctx, HoldNote obj, int y)
        {
            var bodyImg = NoteFieldSettings.NoteSkin.Keys[Index.Value].HoldNoteBody;
            var bodyPattern = new Cairo.SurfacePattern(bodyImg.Surface) { Extend = Cairo.Extend.Repeat };
            var headImg = NoteFieldSettings.NoteSkin.Keys[Index.Value].HoldNote;

            // Reposition the note based on the notefield baseline.
            var headY = y - (int)(NoteFieldSettings.BaseLine * headImg.Pixbuf.Height);

            // The body is drawn starting at the center of the head, so we need to offset it by half the height.
            var bodyY = headY + (int)(headImg.Pixbuf.Height / 2.0);

            // Draw the body.
            ctx.Cairo.SetSource(bodyPattern);
            ctx.Cairo.Rectangle(0, bodyY, NoteFieldSettings.KeyWidth, NoteFieldSettings.BeatToPosition(obj.EndBeat) - bodyY);
            ctx.Cairo.Fill();

            // Draw the hold note head.
            ctx.Cairo.SetSourceSurface(headImg.Surface, 0, headY);
            ctx.Cairo.Paint();
        }
    }
}
