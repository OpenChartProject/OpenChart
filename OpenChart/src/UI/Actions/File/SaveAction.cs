using Serilog;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the application to save the current project.
    /// </summary>
    public class SaveAction : Actions.IAction
    {
        IApplication app;

        public const string Hotkey = "<Control>S";
        public string GetHotkey() => Hotkey;

        public const string Name = "file.save";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new SaveAction instance.
        /// </summary>
        public SaveAction(IApplication app)
        {
            this.app = app;

            _action = new GLib.SimpleAction(Name, null);
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
        }
    }
}
