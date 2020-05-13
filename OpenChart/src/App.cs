using ManagedBass;
using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.NoteSkins;
using OpenChart.UI.Windows;
using Serilog;
using System;
using System.Globalization;
using System.IO;
using System.Threading;

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
        public static bool Init()
        {
            var culture = new CultureInfo("en-US");

            // Use en-US locale throughout the app. We still retain the user's locale with the
            // `DefaultThreadCurrentUICulture` property. This override ensures that things like
            // file formats consistently use the same formatting.
            CultureInfo.DefaultThreadCurrentCulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;

            // Get the path to the folder where the executable is.
            AppFolder = Path.GetDirectoryName(
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
            );

            Directory.SetCurrentDirectory(AppFolder);

            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ShortLevelEnricher())
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    "logs/OpenChart.log",
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] {ShortLevel}   {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();

            Log.Information("------------------------");
            Log.Information("Initializing...");
            Log.Debug($"Set current directory to {AppFolder}");

            try
            {
                // Initialize libbass
                if (!Bass.Init())
                {
                    var error = Enum.GetName(typeof(ManagedBass.Errors), Bass.LastError);

                    Log.Fatal($"Failed to initialize libbass. ({error}, code = {Bass.LastError})");
                    return false;
                }
            }
            catch (DllNotFoundException e)
            {
                Log.Fatal(e, "Failed to initialize libbass (DLL not found).");
                return false;
            }

            Log.Information("libbass init OK.");

            try
            {
                Gtk.Application.Init();
            }
            catch (TypeInitializationException e)
            {
                var msg = "Failed to initialize Gtk";

                if (e.InnerException is DllNotFoundException)
                    msg += " (DLL not found)";

                Log.Fatal(e, msg);
                return false;
            }

            Log.Information("Gtk init OK.");

            Formats = new FormatManager();
            Formats.AddFormat(new OpenChartFormatHandler());

            NoteSkins = new NoteSkinManager();

            Log.Information("Finding noteskins...");
            NoteSkins.LoadAll();

            Log.Information("OpenChart init OK.");

            return true;
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
