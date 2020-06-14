using Serilog;

namespace OpenChart.UI.MenuActions
{
    /// <summary>
    /// An action that triggers the application to quit.
    /// </summary>
    public class QuitAction : MenuActions.IAction
    {
        IApplication app;

        public const string Hotkey = "<Control>Q";
        public string GetHotkey() => Hotkey;

        public const string Name = "file.quit";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new QuitAction instance.
        /// </summary>
        public QuitAction(IApplication app)
        {
            this.app = app;

            _action = new GLib.SimpleAction(Name, null);
            _action.Activated += OnActivated;
            _action.Enabled = true;
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            Log.Debug($"{this.GetType().Name} triggered.");

            // TODO: Handle save logic.
            Gtk.Application.Quit();
        }
    }
}
