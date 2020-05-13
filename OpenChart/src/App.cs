using ManagedBass;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.NoteSkins;
using OpenChart.UI.Windows;
using Serilog;
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

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    "logs/OpenChart.log",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            Log.Information("------------------------");
            Log.Information("Initializing...");
            Log.Debug($"Set current directory to {AppFolder}");

            // Initialize libbass
            if (!Bass.Init())
            {
                Log.Fatal("Failed to initialize libbass.");
                Environment.Exit(1);
            }

            Log.Information("libbass init OK.");

            Gtk.Application.Init();
            Log.Information("Gtk init OK.");

            Formats = new FormatManager();
            Formats.AddFormat(new OpenChartFormatHandler());

            NoteSkins = new NoteSkinManager();

            Log.Information("Finding noteskins...");
            NoteSkins.LoadAll();

            Log.Information("OpenChart init OK.");
        }

        /// <summary>
        /// Runs the app and displays the main window.
        /// </summary>
        public static void Run()
        {
            Log.Information("Displaying main window.");
            new MainWindow();
            Gtk.Application.Run();
        }

        /// <summary>
        /// Quits the app.
        /// </summary>
        public static void Quit()
        {
            Log.Information("Quitting.");
            Gtk.Application.Quit();
        }
    }
}
