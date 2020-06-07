using Gtk;
using OpenChart.UI.NoteField;

namespace OpenChart.UI.Windows
{
    public class SandboxWindow : Window
    {
        public SandboxWindow() : base("sandbox")
        {
            DeleteEvent += delegate { Gtk.Application.Quit(); };

            Resize(640, 480);

            var container = new Layout(null, null);
            var chart = new Charting.Chart(4);
            chart.BPMList.BPMs.Add(new Charting.Properties.BPM());
            var dSettings = new DisplaySettings(chart, 200, 96);
            var blSettings = new BeatLineSettings
            {
                BeatLineColor = new Color(0.5, 0.5, 0.5),
                BeatLineThickness = 1,
                MeasureLineColor = new Color(1, 1, 1),
                MeasureLineThickness = 2
            };
            var beatLines = new BeatLines(dSettings, blSettings);
            container.Put(beatLines.GetWidget(), 0, 0);
            Add(container);
            ShowAll();

            var x = 0.0;
            var y = 0.0;

            container.ScrollEvent += (o, e) =>
            {
                x += e.Event.DeltaX * 25;
                y -= e.Event.DeltaY * 25;

                container.Move(beatLines.GetWidget(), (int)x, (int)y);
            };
        }
    }
}
