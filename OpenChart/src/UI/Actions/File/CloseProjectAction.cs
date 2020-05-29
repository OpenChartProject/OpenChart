namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the current active project to close. This action
    /// is only enabled when an active project is open.
    /// </summary>
    public class CloseProjectAction : Actions.IAction
    {
        public const string Hotkey = null;
        public string GetHotkey() => Hotkey;

        public const string Name = "file.close_project";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new CloseProjectAction instance.
        /// </summary>
        public CloseProjectAction()
        {
            _action = new GLib.SimpleAction(GetName(), null);
            _action.Activated += OnActivated;
            _action.Enabled = false;

            // Set the close project action to be enabled only if there is an active project open.
            Application.GetInstance().EventBus.CurrentProjectChanged += (o, e) =>
            {
                _action.Enabled = (e.NewProject != null);
            };
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            var app = Application.GetInstance();

            app.AppData.CloseCurrentProject();
        }
    }
}
