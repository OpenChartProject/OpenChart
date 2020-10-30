using OpenChart.UI.MenuActions;
using OpenChart.UI.Windows;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenChart
{
    /// <summary>
    /// The main application class. This class is responsible for initializing and bootstrapping
    /// the app by loading in any necessary resources. It should not contain domain logic.
    /// </summary>
    public class Application : Gtk.Application, IApplication
    {
        public const string AppId = "io.openchart";

        /// <summary>
        /// A dictionary of action names --> actions.
        /// </summary>
        public Dictionary<string, IMenuAction> ActionDict { get; private set; }

        ApplicationData applicationData;
        public ApplicationData GetData() => applicationData;

        ApplicationEventBus eventBus;
        public ApplicationEventBus GetEvents() => eventBus;

        public Gtk.Application GetGtk() => this;

        Gtk.Window mainWindow;
        public Gtk.Window GetMainWindow() => mainWindow;

        /// <summary>
        /// The relative path where logs are written to.
        /// </summary>
        public string LogFile { get; private set; }

        public Application() : base(AppId, GLib.ApplicationFlags.None)
        {
            ActionDict = new Dictionary<string, IMenuAction>();
            LogFile = Path.Combine("logs", "OpenChart.log");
        }

        /// <summary>
        /// This method is called after Gtk.Application.Quit() is called. It's the last thing
        /// that runs before the program ends.
        /// </summary>
        public void Cleanup()
        {
            Log.Information("Shutting down...");
        }

        /// <summary>
        /// Initializes the actions for the application.
        /// </summary>
        public void InitActions()
        {
            // File actions
            addMenuAction(new NewProjectAction(this));
            addMenuAction(new NewChartAction(this));
            addMenuAction(new CloseProjectAction(this));
            addMenuAction(new SaveAction(this));
            addMenuAction(new SaveAsAction(this));
            addMenuAction(new QuitAction(this));
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

            applicationData = new ApplicationData(path);
            applicationData.Init();

            eventBus = new ApplicationEventBus(applicationData);

            // Actions should be initialized last since they may require other parts of the application
            // during setup.
            InitActions();

            Log.Information("Ready.");

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

            mainWindow = new MainWindow(this);

            AddWindow(mainWindow);
            mainWindow.ShowAll();
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

        private void addMenuAction(IMenuAction action)
        {
            // FIXME: Can't add accelerators/hotkeys since the Gtk wrapper takes the wrong
            // type of argument, resulting in a segfault.
            ActionDict.Add(action.GetName(), action);
            AddAction(action.Action);
        }
    }
}
