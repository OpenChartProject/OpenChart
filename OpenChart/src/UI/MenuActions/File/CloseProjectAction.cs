namespace OpenChart.UI.MenuActions
{
    /// <summary>
    /// An action that triggers the current active project to close. This action
    /// is only enabled when an active project is open.
    /// </summary>
    public class CloseProjectAction : MenuActions.IMenuAction
    {
        IApplication app;

        public const string Hotkey = null;
        public string GetHotkey() => Hotkey;

        public const string Name = "file.close_project";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new CloseProjectAction instance.
        /// </summary>
        /// <param name="app">The application instance.</param>
        public CloseProjectAction(IApplication app)
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
            app.GetData().CloseCurrentProject();
        }
    }
}
