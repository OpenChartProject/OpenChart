using GLib;

namespace OpenChart.UI.Actions
{
    public class ExitAction : SimpleAction
    {
        public const string Hotkey = "<Control>Q";

        public ExitAction() : base("exit", null)
        {
            // Enabled = true;
        }

        protected override void OnActivated(Variant variant)
        {
            Gtk.Application.Quit();
        }
    }
}
