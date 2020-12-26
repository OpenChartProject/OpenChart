using OpenChart.Charting.Properties;

namespace OpenChart.UI.Components.NoteField
{
    public class Receptor : Component
    {
        public KeyIndex Index { get; private set; }

        public NoteFieldSettings NoteFieldSettings { get; private set; }

        public Receptor(NoteFieldSettings noteFieldSettings, KeyIndex index)
        {
            Index = index;
            NoteFieldSettings = noteFieldSettings;
        }

        public override void Draw(Cairo.Context ctx)
        {
            var img = NoteFieldSettings.NoteSkin.ScaledKeys[Index.Value].Receptor;

            // Reposition the receptor based on the notefield baseline.
            var offsetY = (int)(NoteFieldSettings.BaseLine * img.Width);

            ctx.SetSourceSurface(img.CairoSurface, 0, -offsetY);
            ctx.Paint();
        }
    }
}
