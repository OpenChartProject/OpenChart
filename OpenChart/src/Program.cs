using Serilog;
using System;

namespace OpenChart
{
    /// <summary>
    /// Main entrypoint.
    /// </summary>
    class Program
    {
        public static void Main()
        {
            Application gtkApp;

            try
            {
                gtkApp = new Application();
            }
            catch (TypeInitializationException e)
            {
                var msg = "Failed to initialize Gtk";

                if (e.InnerException is DllNotFoundException)
                    msg += " (DLL not found)";

                Log.Fatal(e, msg);
                Environment.Exit(1);
            }

            try
            {
                Gtk.Application.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Uncaught exception.");
            }
        }
    }
}
