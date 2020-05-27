using GLib;

namespace OpenChart.UI.Actions
{
    public class FileActionGroup : SimpleActionGroup
    {
        public FileActionGroup()
        {
            this.AddAction(new ExitAction());
        }
    }
}
