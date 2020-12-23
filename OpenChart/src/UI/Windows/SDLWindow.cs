using static SDL2.SDL;
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
            Handle = SDL_CreateWindow(
                baseTitle,
                SDL_WINDOWPOS_CENTERED,
                SDL_WINDOWPOS_CENTERED,
                initialWidth,
                initialHeight,
                SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (Handle == IntPtr.Zero)
            {
                var msg = string.Format("Failed to create window: {0}", SDL_GetError());
                Log.Fatal(msg);
                throw new Exception(msg);
            }

            SDL_SetWindowMinimumSize(Handle, minWidth, minHeight);
        }

        /// <summary>
        /// Swaps the buffer for the surface, displaying whatever was drawn on the surface.
        /// </summary>
        public void SwapBuffer()
        {
            SDL_UpdateWindowSurface(Handle);
        }

        /// <summary>
        /// Disposes the current surface and creates a new one.
        /// </summary>
        public void RefreshSurface()
        {
            Surface?.Dispose();
            Surface = new Surface(SDL_GetWindowSurface(Handle), freeOnDispose: false);
        }
    }
}
