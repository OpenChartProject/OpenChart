using Gtk;
using ChartObjects = OpenChart.Charting.Objects;
using OpenChart.UI.NoteField;

namespace OpenChart.UI.Windows
{
    public class SandboxWindow : Window
    {
        public SandboxWindow(IApplication app) : base("sandbox")
        {
            DeleteEvent += delegate { Gtk.Application.Quit(); };

            Resize(640, 480);

            var chart = new Charting.Chart(4);
            chart.BPMList.BPMs.Add(new Charting.Properties.BPM());

            var noteSkin = app.GetData().NoteSkins.GetNoteSkin("default_arrow").GetKeyModeSkin(chart.KeyCount);

            var noteFieldSettings = new NoteFieldSettings(
                chart,
                noteSkin,
                200,
                96,
                NoteFieldObjectAlignment.Center
            );

            var beatLineSettings = new BeatLineSettings
            {
                BeatLineColor = new Color(0.5, 0.5, 0.5),
                BeatLineThickness = 1,
                MeasureLineColor = new Color(1, 1, 1),
                MeasureLineThickness = 2
            };

            var noteField = new NoteField.NoteField(noteFieldSettings);

            noteField.ShowBeatLines(beatLineSettings);
            noteField.ShowKeys();

            var container = new Layout(null, null);
            container.Add(noteField.GetWidget());
            Add(container);

            ShowAll();

            var x = 0.0;
            var y = 0.0;

            container.ScrollEvent += (o, e) =>
            {
                x += e.Event.DeltaX * 25;
                y -= e.Event.DeltaY * 25;

                container.Move(noteField.GetWidget(), (int)x, (int)y);
            };

            chart.Objects[0].Add(new ChartObjects.TapNote(0, 0));
            chart.Objects[1].Add(new ChartObjects.TapNote(1, 0));
            chart.Objects[2].Add(new ChartObjects.TapNote(2, 0));
            chart.Objects[3].Add(new ChartObjects.TapNote(3, 0));

            chart.Objects[0].Add(new ChartObjects.TapNote(0, 1));
            chart.Objects[1].Add(new ChartObjects.TapNote(1, 1.25));
            chart.Objects[2].Add(new ChartObjects.TapNote(2, 1.5));
            chart.Objects[3].Add(new ChartObjects.TapNote(3, 1.75));

            chart.Objects[0].Add(new ChartObjects.HoldNote(0, 2, 2.4));
        }
    }
}
