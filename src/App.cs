using OpenChart.NoteSkin;
using OpenChart.UI;
using ManagedBass;
using System;

namespace OpenChart
{
    /// <summary>
    /// A static class that contains application data.
    /// </summary>
    public static class App
    {
        public const string NoteSkinFolder = "noteskins";
        public static NoteSkinManager NoteSkins { get; private set; }

        public static void Init()
        {
            if (!Bass.Init())
            {
                Console.WriteLine("Failed to initialize libbass");
                App.Quit();
            }

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