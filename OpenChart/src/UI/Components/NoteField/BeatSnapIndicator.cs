using Cairo;

namespace OpenChart.UI.Components.NoteField
{
    public class BeatSnapIndicator : Component
    {
        public NoteFieldSettings Settings;

        public BeatSnapIndicator(NoteFieldSettings settings)
        {
            Settings = settings;
        }

        public override void Draw(Context ctx)
        {
            ctx.Translate(Settings.NoteFieldWidth + 30, 0);

            ctx.SetFontSize(20);
            ctx.SetSourceRGB(1, 1, 1);
            ctx.ShowText(string.Format("1/{0}", Settings.BeatSnap.Value));
        }
    }
}
