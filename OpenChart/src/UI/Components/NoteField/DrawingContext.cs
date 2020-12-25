using OpenChart.Charting.Properties;

namespace OpenChart.UI.Components.NoteField
{
    public class DrawingContext
    {
        public Cairo.Context Cairo { get; private set; }

        public BeatTime Top { get; private set; }

        public BeatTime Bottom { get; private set; }

        public DrawingContext(Cairo.Context ctx, BeatTime top, BeatTime bottom)
        {
            Cairo = ctx;
            Top = top;
            Bottom = bottom;
        }
    }
}
