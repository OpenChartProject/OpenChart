using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;

namespace OpenChart.UI.Components.NoteField
{
    public class Key : Component
    {
        public KeyIndex Index { get; private set; }

        public NoteFieldSettings NoteFieldSettings { get; private set; }

        public Key(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            Index = index;
            NoteFieldSettings = noteFieldSettings;
        }

        public override void Draw(Cairo.Context ctx)
        {
            // This controls how much of a vertical margin we are giving ourselves to draw beyond
            // the screen. This fixes an issue where an object close to the edge of the screen may
            // not be drawn correctly.
            var margin = 100;
            var iter = NoteFieldSettings.Chart.Objects[Index.Value].GetEnumerator();

            while (iter.MoveNext())
            {
                var cur = iter.Current;

                if ((cur.Time.Value + margin) < NoteFieldSettings.Top.Time.Value)
                    continue;
                else if ((cur.Time.Value - margin) > NoteFieldSettings.Bottom.Time.Value)
                    break;

                drawObject(ctx, cur);
            }
        }

        private void drawObject(Cairo.Context ctx, BaseObject obj)
        {
            var y = NoteFieldSettings.TimeToPosition(obj.Time);

            if (obj is TapNote tapNote)
                drawTapNote(ctx, tapNote, y);
            else if (obj is HoldNote holdNote)
                drawHold(ctx, holdNote, y);
        }

        private void drawTapNote(Cairo.Context ctx, TapNote obj, int y)
        {
            var img = NoteFieldSettings.NoteSkin.ScaledKeys[Index.Value].TapNote;

            // Reposition the note based on the notefield baseline.
            var offsetY = y - (int)(NoteFieldSettings.BaseLine * img.Width);

            ctx.SetSourceSurface(img.CairoSurface, 0, offsetY);
            ctx.Paint();
        }

        private void drawHold(Cairo.Context ctx, HoldNote obj, int y)
        {
            var bodyImg = NoteFieldSettings.NoteSkin.ScaledKeys[Index.Value].HoldNoteBody;
            var headImg = NoteFieldSettings.NoteSkin.ScaledKeys[Index.Value].HoldNote;

            // Reposition the note based on the notefield baseline.
            var headY = y - (int)(NoteFieldSettings.BaseLine * headImg.Height);

            // The body is drawn starting at the center of the head, so we need to offset it by half the height.
            var bodyY = headY + (int)(headImg.Height / 2.0);

            // Draw the body.
            ctx.SetSource(bodyImg.Pattern);
            ctx.Rectangle(0, bodyY, NoteFieldSettings.KeyWidth, NoteFieldSettings.BeatToPosition(obj.EndBeat) - bodyY);
            ctx.Fill();

            // Draw the hold note head.
            ctx.SetSourceSurface(headImg.CairoSurface, 0, headY);
            ctx.Paint();
        }
    }
}
