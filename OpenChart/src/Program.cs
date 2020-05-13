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
                App.Init();
                App.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Uncaught exception.");
            }
        }
    }
}
