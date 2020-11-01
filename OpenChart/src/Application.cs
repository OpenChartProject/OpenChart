using OpenChart.UI.MenuActions;
using OpenChart.UI.Windows;
using SDL2;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace OpenChart
{
    /// <summary>
    /// The main application class. This class is responsible for initializing and bootstrapping
    /// the app by loading in any necessary resources.
    /// </summary>
    public class Application : IApplication
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

        public Gtk.Application GetGtk() => null;

        SDLWindow mainWindow;
        public Gtk.Window GetMainWindow() => null;

        /// <summary>
        /// The relative path where logs are written to.
        /// </summary>
        public string LogFile { get; private set; }

        public Application()
        {
            ActionDict = new Dictionary<string, IMenuAction>();
            LogFile = Path.Combine("logs", "OpenChart.log");
        }

        /// <summary>
        /// Final cleanup before the program exits.
        /// </summary>
        public void Cleanup()
        {
            Log.Information("Shutting down...");
        }

        public void Run()
        {
            if (!init())
            {
                Log.Fatal("Failed to initialize app, quitting...");
                Environment.Exit(1);
            }

            Log.Information("Displaying main window.");
            mainWindow = new SDLWindow();

            var quit = false;

            // Main event loop.
            while (!quit)
            {
                SDL.SDL_Event e;
                SDL.SDL_PollEvent(out e);

                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        // TODO: Check if there are unsaved changes.
                        quit = true;
                        break;
                }
            }
        }

        bool init()
        {
            var path = setCurrentDirectory();
            initLogging();

            Log.Information("------------------------");
            Log.Information("Initializing...");
            Log.Debug($"Set current directory to {path}");

            applicationData = new ApplicationData(path);
            applicationData.Init();

            eventBus = new ApplicationEventBus(applicationData);

            // Actions should be initialized last since they may require other parts of the application
            // during setup.
            // initActions();

            Log.Information("Ready.");

            return true;
        }

        void initActions()
        {
            // File actions
            addMenuAction(new NewProjectAction(this));
            addMenuAction(new NewChartAction(this));
            addMenuAction(new CloseProjectAction(this));
            addMenuAction(new SaveAction(this));
            addMenuAction(new SaveAsAction(this));
            addMenuAction(new QuitAction(this));
        }

        void initLogging()
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

        string setCurrentDirectory()
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

        private void addMenuAction(IMenuAction action)
        {
            // FIXME: Can't add accelerators/hotkeys since the Gtk wrapper takes the wrong
            // type of argument, resulting in a segfault.
            // ActionDict.Add(action.GetName(), action);
            // AddAction(action.Action);
        }
    }
}
