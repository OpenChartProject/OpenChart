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
            var app = new Application();

            try
            {
                app.Run();
            }
            catch (Exception e)
            {
                Log.Fatal("Uncaught exception: {0}", e);
                Environment.Exit(1);
            }

            app.Cleanup();
        }
    }
}
