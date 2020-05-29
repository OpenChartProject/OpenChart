using ManagedBass;
using OpenChart.UI.Actions;
using OpenChart.UI.Windows;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenChart
{
    /// <summary>
    /// The main application class (singleton). The purpose of this class is to handle all
    /// of the startup initialization and act as a central source of truth for resources that
    /// are shared throughout the app.
    ///
    /// This class should not contain any domain logic. It's only responsibility is to bootstrap
    /// the application and get everything setup.
    /// </summary>
    public class Application : Gtk.Application
    {
        static Application instance;

        /// <summary>
        /// A dictionary of action names --> actions.
        /// </summary>
        public Dictionary<string, IAction> ActionDict { get; private set; }

        /// <summary>
        /// Internal data used by the app.
        /// </summary>
        public ApplicationData AppData { get; private set; }

        /// <summary>
        /// An event bus for application-wide events.
        /// </summary>
        public ApplicationEventBus EventBus { get; private set; }

        /// <summary>
        /// The relative path where logs are written to.
        /// </summary>
        public string LogFile { get; private set; }

        private Application() : base("io.openchart", GLib.ApplicationFlags.None)
        {
            ActionDict = new Dictionary<string, IAction>();
            LogFile = Path.Combine("logs", "OpenChart.log");
        }

        /// <summary>
        /// This method is called after Gtk.Application.Quit() is called. It's the last thing
        /// that runs before the program ends.
        /// </summary>
        public void Cleanup()
        {
            // TODO: Handle save logic.
            Log.Information("Shutting down...");
        }

        /// <summary>
        /// Initializes the actions for the application.
        /// </summary>
        public void InitActions()
        {
            // File actions
            addAction(new NewProjectAction());
            addAction(new CloseProjectAction());
            addAction(new QuitAction());
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

            EventBus = new ApplicationEventBus(AppData);

            // Actions should be initialized last since they may require other parts of the application
            // during setup.
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
        /// A singleton Application instance.
        /// </summary>
        public static Application GetInstance()
        {
            if (instance == null)
                instance = new Application();

            return instance;
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

        private void addAction(IAction action)
        {
            // FIXME: Can't add accelerators/hotkeys since the Gtk wrapper takes the wrong
            // type of argument, resulting in a segfault.
            ActionDict.Add(action.GetName(), action);
            AddAction(action.Action);
        }
    }
}
