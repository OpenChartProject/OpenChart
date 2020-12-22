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
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// The window surface. This surface is invalidated when the window is resized, so anything
        /// that uses this surface should not cache it.
        /// </summary>
        public Surface Surface { get; private set; }

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
        }

        /// <summary>
        /// Paint swaps the buffer for the window surface.
        /// </summary>
        public void Paint()
        {
            SDL.SDL_UpdateWindowSurface(Handle);
        }

        /// <summary>
        /// Checks the window to see if the surface has been invalidated. If it has, the surface
        /// is disposed and a new one is created.
        /// </summary>
        public void RefreshSurface()
        {
            var windowSurface = SDL.SDL_GetWindowSurface(Handle);

            if (Surface != null)
            {
                // The window surface hasn't changed and is still valid.
                if (windowSurface == Surface.Data)
                    return;

                Surface.Dispose();
            }

            Surface = new Surface(windowSurface, freeOnDispose: false);
        }
    }
}
