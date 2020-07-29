using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;

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
            var iter = NoteFieldSettings.Chart.Objects[Index.Value].GetEnumerator();

            while (iter.Current != null)
            {
                var cur = iter.Current;

                if (cur.Beat.Value < ctx.Top.Beat.Value)
                    continue;
                else if (cur.Beat.Value > ctx.Bottom.Beat.Value)
                    break;

                drawObject(ctx, cur);

                iter.MoveNext();
            }
        }

        private void drawObject(DrawingContext ctx, BaseObject obj)
        {
            var y = obj.Time.Value * NoteFieldSettings.PixelsPerSecond;

            if (obj is TapNote)
            {
                var pixbuf = NoteFieldSettings.NoteSkin.Keys[Index.Value].TapNote.Pixbuf;
                Gdk.CairoHelper.SetSourcePixbuf(ctx.Cairo, pixbuf, 0, 0);
                ctx.Cairo.Rectangle(
                    new Cairo.Rectangle(
                        ctx.Cairo.ClipExtents().X,
                        y,
                        pixbuf.Width,
                        pixbuf.Height
                    )
                );
                ctx.Cairo.Fill();
            }
        }
    }
}
