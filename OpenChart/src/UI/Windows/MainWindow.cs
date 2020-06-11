using Gtk;
using ChartObjects = OpenChart.Charting.Objects;
using OpenChart.UI.Actions;
using OpenChart.UI.NoteField;

namespace OpenChart.UI.Windows
{
    /// <summary>
    /// The main window of the application.
    /// </summary>
    public class MainWindow : Window
    {
        IApplication app;
        VBox container;

        const string baseTitle = "OpenChart";

        const int InitialWindowWidth = 800;
        const int InitialWindowHeight = 600;
        const int MinimumWindowWidth = 360;
        const int MinimumWindowHeight = 240;

        public MainWindow(IApplication app) : base(baseTitle)
        {
            this.app = app;

            app.GetEvents().CurrentProjectChanged += delegate { renameWindowToMatchProject(); };
            app.GetEvents().CurrentProjectRenamed += delegate { renameWindowToMatchProject(); };
            DeleteEvent += onDelete;

            SetIconFromFile(System.IO.Path.Join("icons", "AppIcon.ico"));

            var chart = new Charting.Chart(4);
            chart.BPMList.BPMs.Add(new Charting.Properties.BPM());

            chart.Objects[0].Add(new ChartObjects.TapNote(0, 0));
            chart.Objects[1].Add(new ChartObjects.TapNote(1, 0));
            chart.Objects[2].Add(new ChartObjects.TapNote(2, 0));
            chart.Objects[3].Add(new ChartObjects.TapNote(3, 0));

            chart.Objects[0].Add(new ChartObjects.TapNote(0, 1));
            chart.Objects[1].Add(new ChartObjects.TapNote(1, 1.25));
            chart.Objects[2].Add(new ChartObjects.TapNote(2, 1.5));
            chart.Objects[3].Add(new ChartObjects.TapNote(3, 1.75));

            chart.Objects[0].Add(new ChartObjects.HoldNote(0, 2, 2.4));

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

            noteField.EnableBeatLines(beatLineSettings);
            noteField.EnableKeys();

            var view = new NoteFieldView(noteField);



            container = new VBox();
            container.PackStart(new Widgets.MenuBar(new MenuModel().GetModel()), false, false, 0);
            container.Add(view.GetWidget());

            Add(container);

            SetGeometryHints(
                null,
                new Gdk.Geometry
                {
                    MinWidth = MinimumWindowWidth,
                    MinHeight = MinimumWindowHeight,
                },
                Gdk.WindowHints.MinSize
            );

            SetDefaultSize(InitialWindowWidth, InitialWindowHeight);
            SetPosition(WindowPosition.Center);
        }

        private void renameWindowToMatchProject()
        {
            var project = app.GetData().CurrentProject;
            var title = baseTitle;

            if (project != null)
                title += " - " + project.Name;

            Title = title;
        }

        private void onDelete(object o, DeleteEventArgs e)
        {
            app.GetGtk().ActivateAction(QuitAction.Name, null);
        }
    }
}
