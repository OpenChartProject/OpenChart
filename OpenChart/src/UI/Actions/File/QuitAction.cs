using GLib;

namespace OpenChart.UI.Actions
{
    public class QuitAction : Actions.IAction
    {
        public static string Hotkey => "<Control>Q";
        public static string Name => "file.quit";

        SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

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
