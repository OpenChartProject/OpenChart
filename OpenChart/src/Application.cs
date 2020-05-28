using ManagedBass;
using OpenChart.UI.Windows;
using Serilog;
using System;
using System.IO;

namespace OpenChart
{
    public class Application : Gtk.Application
    {
        public static Application Instance;
        public ApplicationData AppData { get; private set; }

        public Application() : base("io.openchart", GLib.ApplicationFlags.None)
        {
            Application.Instance = this;
            AppData = new ApplicationData();

            Register(GLib.Cancellable.Current);
        }

        public bool InitApplication()
        {
            SetApplicationDir();
            InitLogging();

            Log.Information("------------------------");
            Log.Information("Initializing...");
            Log.Debug($"Set current directory to {AppData.AppFolder}");

            if (!InitAudio())
                return false;

            AppData.Init();

            Log.Information("Initialization complete.");

            return true;
        }

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

        public void InitLogging()
        {
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
        }

        public void SetApplicationDir()
        {
            // Get the path to the folder where the executable is.
            AppData.AppFolder = Path.GetDirectoryName(
                System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName
            );

            Directory.SetCurrentDirectory(AppData.AppFolder);
        }

        protected override void OnActivated()
        {
            Log.Information("Displaying main window.");

            var window = new MainWindow();

            AddWindow(window);
            window.ShowAll();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();

            Log.Information("OnShutdown");
        }

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
