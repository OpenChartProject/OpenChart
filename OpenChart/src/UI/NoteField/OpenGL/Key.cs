using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using Serilog;
using System;

namespace OpenChart.UI.NoteField.OpenGL
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
            // not be drawn if say the object's origin is off screen but the object itself should
            // still be visible.
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
        }

        private void drawTapNote(DrawingContext ctx, TapNote obj, int y)
        {
            var image = NoteFieldSettings.NoteSkin.Keys[Index.Value].TapNote;
            var pixbuf = image.Pixbuf;

            ctx.Cairo.SetSourceSurface(
                image.Surface,
                0,
                (int)Math.Round(y - NoteFieldSettings.Align(pixbuf.Height))
            );

            ctx.Cairo.Paint();
        }
    }
}
