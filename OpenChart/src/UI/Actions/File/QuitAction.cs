using GLib;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the application to quit.
    /// </summary>
    public class QuitAction : Actions.IAction
    {
        public static string Hotkey => "<Control>Q";
        public static string Name => "file.quit";

        SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new QuitAction instance.
        /// </summary>
        public QuitAction()
        {
            _action = new SimpleAction(Name, null);
            _action.Activated += OnActivated;
            _action.Enabled = true;
        }

        protected void OnActivated(object o, ActivatedArgs args)
        {
            Gtk.Application.Quit();
        }
    }
}
