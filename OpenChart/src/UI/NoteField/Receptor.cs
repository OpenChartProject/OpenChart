using OpenChart.Charting.Properties;

namespace OpenChart.UI.NoteField
{
    public class Receptor
    {
        public KeyIndex Index { get; private set; }

        public NoteFieldSettings NoteFieldSettings { get; private set; }

        public Receptor(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            Index = index;
            NoteFieldSettings = noteFieldSettings;
        }

        public void Draw(DrawingContext ctx)
        {
            var img = NoteFieldSettings.NoteSkin.ScaledKeys[Index.Value].Receptor;

            ctx.Cairo.SetSource(img.CairoSurface);
            ctx.Cairo.Paint();
        }
    }
}
