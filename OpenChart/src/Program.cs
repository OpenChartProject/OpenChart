using SDL2;
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
            if (SDL.SDL_Init(SDL.SDL_INIT_AUDIO | SDL.SDL_INIT_VIDEO) != 0)
            {
                Log.Fatal("Failed to initialize SDL:", SDL.SDL_GetError());
                Environment.Exit(1);
            }

            var app = new Application();

            app.Register(GLib.Cancellable.Current);
            app.Activate();

            try
            {
                Gtk.Application.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Uncaught exception.");
                Environment.Exit(1);
            }

            app.Cleanup();
        }
    }
}
