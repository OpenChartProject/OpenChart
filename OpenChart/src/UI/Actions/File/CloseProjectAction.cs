namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the current active project to close. This action
    /// is only enabled when an active project is open.
    /// </summary>
    public class CloseProjectAction : Actions.IAction
    {
        public static string Hotkey => null;
        public string GetHotkey() => Hotkey;

        public static string Name => "file.close_project";
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
        }

        public void SetEnabled(bool enabled)
        {
            _action.Enabled = enabled;
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            var app = Application.GetInstance();

            app.AppData.CloseCurrentProject();
        }
    }
}
