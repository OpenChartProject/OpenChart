using ManagedBass;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.NoteSkins;
using OpenChart.UI;
using System;
using System.IO;
using System.Reflection;

namespace OpenChart
{
    /// <summary>
    /// A static class that contains application data.
    /// </summary>
    public static class App
    {
        /// <summary>
        /// The absolute path to the folder where the OpenChart executable is.
        /// </summary>
        public static string AppFolder { get; private set; }

        /// <summary>
        /// The manager for different file formats.
        /// </summary>
        public static FormatManager Formats { get; private set; }

        /// <summary>
        /// The location of the noteskins folder.
        /// </summary>
        public static string NoteSkinFolder => "noteskins";

        /// <summary>
        /// The noteskins that are loaded into the app.
        /// </summary>
        public static NoteSkinManager NoteSkins { get; private set; }

        /// <summary>
        /// Initializes the app.
        /// </summary>
        public static void Init()
        {
            // Get the path to the folder where the executable is.
            AppFolder = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location
            );

            Directory.SetCurrentDirectory(AppFolder);

            // Initialize libbass
            if (!Bass.Init())
            {
                Console.WriteLine("Failed to initialize libbass");
                App.Quit();
            }

            Gtk.Application.Init();

            Formats = new FormatManager();
            Formats.AddFormat(new OpenChartFormatHandler());

            NoteSkins = new NoteSkinManager();
            NoteSkins.LoadAll();
        }

        /// <summary>
        /// Runs the app and displays the main window.
        /// </summary>
        public static void Run()
        {
            new MainWindow();
            Gtk.Application.Run();
        }

        /// <summary>
        /// Quits the app.
        /// </summary>
        public static void Quit()
        {
            Gtk.Application.Quit();
        }
    }
}