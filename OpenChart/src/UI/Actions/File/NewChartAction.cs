using OpenChart.Charting;
using Serilog;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the application to create a new chart.
    /// </summary>
    public class NewChartAction : Actions.IAction
    {
        IApplication app;

        public const string Hotkey = "<Control><Shift>n";
        public string GetHotkey() => Hotkey;

        public const string Name = "file.new_chart";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new NewChartAction instance.
        /// </summary>
        public NewChartAction(IApplication app)
        {
            this.app = app;

            _action = new GLib.SimpleAction(GetName(), null);
            _action.Activated += OnActivated;
            _action.Enabled = false;

            // Enable the action only when a project is open.
            app.GetEvents().CurrentProjectChanged += (o, e) =>
            {
                _action.Enabled = (e.NewProject != null);
            };
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            Log.Debug($"{this.GetType().Name} triggered.");

            // Add a new blank 4k chart.
            app.GetData().CurrentProject.AddChart(new Chart(4));
        }
    }
}
