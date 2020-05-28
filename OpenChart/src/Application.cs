using ManagedBass;
using OpenChart.UI.Actions;
using OpenChart.UI.Windows;
using Serilog;
using System;
using System.IO;

namespace OpenChart
{
    /// <summary>
    /// The main application class.
    /// </summary>
    public class Application : Gtk.Application
    {
        /// <summary>
        /// The application instance. Use this if you need to access the application or
        /// the application data.
        /// </summary>
        public static Application Instance;

        /// <summary>
        /// OpenChart-specific application data.
        /// </summary>
        public ApplicationData AppData { get; private set; }

        /// <summary>
        /// The relative path where logs are written to.
        /// </summary>
        public string LogFile { get; private set; }

        /// <summary>
        /// Creates a new Application instance.
        /// </summary>
        public Application() : base("io.openchart", GLib.ApplicationFlags.None)
        {
            Application.Instance = this;
            LogFile = Path.Combine("logs", "OpenChart.log");

            Register(GLib.Cancellable.Current);
        }

        public void InitActions()
        {
            // FIXME: Can't add accelerators/hotkeys since the Gtk wrapper takes the wrong
            // type of argument, resulting in a segfault.
            AddAction(new QuitAction().Action);
        }

        /// <summary>
        /// Initializes the application. This sets up the components of the app, such as logging,
        /// loading the audio library, loading noteskins, etc.
        /// </summary>
        public bool InitApplication()
        {
            var path = SetApplicationPath();
            InitLogging();

            Log.Information("------------------------");
            Log.Information("Initializing...");
            Log.Debug($"Set current directory to {path}");

            if (!InitAudio())
                return false;

            AppData = new ApplicationData(path);
            AppData.Init();

            InitActions();

            Log.Information("Ready.");

            return true;
        }

        /// <summary>
        /// Initializes the libbass library.
        /// </summary>
        public bool InitAudio()
        {
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

            return true;
        }

        /// <summary>
        /// Initializes logging. <seealso cref="Application.LogFile" />
        /// </summary>
        public void InitLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.With(new ShortLevelEnricher())
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    LogFile,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] {ShortLevel}   {Message:lj}{NewLine}{Exception}"
                )
                .CreateLogger();
        }

        /// <summary>
        /// Sets the current working directory to the path where the executable is. This ensures
        /// that relative paths work correctly.
        /// </summary>
        public string SetApplicationPath()
        {
            var path = Environment.GetEnvironmentVariable("OPENCHART_DIR");

            if (string.IsNullOrEmpty(path))
            {
                // Get the path to the folder where the executable is.
                path = Path.GetDirectoryName(
                    System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
                );
            }

            Directory.SetCurrentDirectory(path);

            return path;
        }

        /// <summary>
        /// Called when the application is ready to be used.
        /// </summary>
        protected override void OnActivated()
        {
            Log.Information("Displaying main window.");

            var window = new MainWindow(this);

            AddWindow(window);
            window.ShowAll();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();

            Log.Information("OnShutdown");
        }

        /// <summary>
        /// Called when the application instance is first created. The application quits if there
        /// is an error during setup.
        /// </summary>
        protected override void OnStartup()
        {
            base.OnStartup();

            if (!InitApplication())
            {
                Log.Fatal("Failed to initialize, quitting...");
                Environment.Exit(1);
            }
        }
    }
}
