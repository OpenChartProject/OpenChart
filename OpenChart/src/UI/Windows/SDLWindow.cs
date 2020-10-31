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
        public readonly IntPtr WindowHandle;

        /// <summary>
        /// A pointer to the SDL renderer for the window.
        /// </summary>
        public readonly IntPtr Renderer;

        const string baseTitle = "OpenChart";

        const int initialWidth = 800;
        const int initialHeight = 600;
        const int minWidth = 360;
        const int minHeight = 240;

        public SDLWindow()
        {
            WindowHandle = SDL.SDL_CreateWindow(
                baseTitle,
                SDL.SDL_WINDOWPOS_CENTERED,
                SDL.SDL_WINDOWPOS_CENTERED,
                initialWidth,
                initialHeight,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE
            );

            if (WindowHandle == null)
            {
                var msg = string.Format("Failed to create window: %s", SDL.SDL_GetError());
                Log.Fatal(msg);
                throw new Exception(msg);
            }

            Renderer = SDL.SDL_CreateRenderer(WindowHandle, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (Renderer == null)
            {
                SDL.SDL_DestroyWindow(WindowHandle);

                var msg = string.Format("Failed to create renderer: %s", SDL.SDL_GetError());
                Log.Fatal(msg);
                throw new Exception(msg);
            }

            SDL.SDL_SetWindowMinimumSize(WindowHandle, minWidth, minHeight);
        }
    }
}
