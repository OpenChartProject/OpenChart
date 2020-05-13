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
            try
            {
                if (!App.Init())
                {
                    Log.Fatal("Initialization failed, quitting...");
                    Environment.Exit(1);
                }

                App.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Uncaught exception.");
            }
        }
    }
}
