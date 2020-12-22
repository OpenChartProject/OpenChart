using SDL2;
using Serilog;
using System;

namespace OpenChart.UI.Windows
{
    public class SDLWindow
    {
        /// <summary>
        /// A pointer to the SDL window.
        /// </summary>
        public readonly IntPtr Handle;

        /// <summary>
        /// The window surface.
        /// </summary>
        public readonly Surface Surface;

        const string baseTitle = "OpenChart";

        const int initialWidth = 800;
        const int initialHeight = 600;
        const int minWidth = 360;
        const int minHeight = 240;

        public SDLWindow()
        {
            Handle = SDL.SDL_CreateWindow(
                baseTitle,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                initialWidth,
                initialHeight,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (Handle == null)
            {
                var msg = string.Format("Failed to create window: %s", SDL.SDL_GetError());
                Log.Fatal(msg);
                throw new Exception(msg);
            }

            SDL.SDL_SetWindowMinimumSize(Handle, minWidth, minHeight);

            Surface = new Surface(SDL.SDL_GetWindowSurface(Handle));
        }

        /// <summary>
        /// Paint swaps the buffer for the window surface.
        /// </summary>
        public void Paint()
        {
            SDL.SDL_UpdateWindowSurface(Handle);
        }
    }
}
