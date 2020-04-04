using charter.NoteSkin;
using charter.UI;

namespace charter
{
    public static class App
    {
        public const string NoteSkinFolder = "noteskins";
        public static NoteSkinManager NoteSkins { get; private set; }

        public static void Init()
        {
            Gtk.Application.Init();
            NoteSkins = new NoteSkinManager();
        }

        public static void Run()
        {
            new MainWindow();
            Gtk.Application.Run();
        }

        public static void Quit()
        {
            Gtk.Application.Quit();
        }
    }
}