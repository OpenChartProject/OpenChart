using SDL2;
using System;

namespace OpenChart.UI.Assets
{
    /// <summary>
    /// ImageData is a simple wrapper class for managing an SDL surface.
    /// </summary>
    public class ImageData : IDisposable
    {
        /// <summary>
        /// The image data which is stored internally as an SDL_Surface.
        /// </summary>
        public IntPtr Data { get; private set; }

        public ImageData(IntPtr data)
        {
            if (data == IntPtr.Zero)
                throw new ArgumentNullException("Surface data cannot be null.");

            Data = data;
        }

        public void Dispose()
        {
            if (Data == IntPtr.Zero)
                return;

            SDL.SDL_FreeSurface(Data);
            Data = IntPtr.Zero;
        }

        /// <summary>
        /// Copies the image data to a new instance that has the given width and height.
        /// </summary>
        public ImageData Resize(int width, int height)
        {
            var surface = SDL.SDL_CreateRGBSurface(0, width, height, 32, 0, 0, 0, 0);

            if (surface == null)
                throw new NullReferenceException($"SDL_CreateRGBSurface returned null: {SDL.SDL_GetError()}");

            var dst = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = width,
                h = height
            };

            SDL.SDL_BlitScaled(Data, IntPtr.Zero, surface, ref dst);

            return new ImageData(surface);
        }
    }
}
