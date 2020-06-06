using Gtk;
using OpenChart.UI.NoteField;

namespace OpenChart.UI.Windows
{
    public class SandboxWindow : Window
    {
        public SandboxWindow() : base("sandbox")
        {
            Resize(640, 480);
            Add(new BeatLines(null, null).GetWidget());
            ShowAll();

            DeleteEvent += delegate { Gtk.Application.Quit(); };
        }
    }
}
