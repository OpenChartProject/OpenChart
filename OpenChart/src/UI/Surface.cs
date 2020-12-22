using SDL2;
using Serilog;
using System;

namespace OpenChart.UI
{
    /// <summary>
    /// Wrapper for SDL and Cairo surfaces.
    /// </summary>
    public class Surface : IDisposable
    {
        bool disposed = false;
        bool freeOnDispose;

        /// <summary>
        /// A pointer to the underlying SDL_Surface* object.
        /// </summary>
        public IntPtr Data { get; private set; }

        /// <summary>
        /// The surface used by Cairo. This references the underlying SDL_Surface* object and is
        /// mostly just a wrapper so that Cairo can use it.
        /// </summary>
        public Cairo.ImageSurface CairoSurface { get; private set; }

        /// <summary>
        /// Creates a new Surface instance.
        /// </summary>
        /// <param name="data">A pointer to an SDL_Surface*.</param>
        public Surface(IntPtr data, bool freeOnDispose = true)
        {
            if (data == IntPtr.Zero)
                throw new ArgumentNullException("Surface data cannot be null.");

            Data = data;
            CairoSurface = createCairoSurface();
            this.freeOnDispose = freeOnDispose;
        }

        ~Surface()
        {
            Dispose(false);
        }

        /// <summary>
        /// Cleans up the surface. If the surface was created with freeOnDispose = true then the
        /// SDL_Surface is also freed.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
                CairoSurface.Dispose();

            if (freeOnDispose)
                SDL.SDL_FreeSurface(Data);

            Data = IntPtr.Zero;
            CairoSurface = null;
            disposed = true;
        }

        /// <summary>
        /// Creates and returns a new Surface with the given width and height. The pixels from this
        /// Surface are scaled and rendered to the new Surface.
        /// </summary>
        public Surface Resize(int width, int height)
        {
            var dst = SDL.SDL_CreateRGBSurface(0, width, height, 32, 0, 0, 0, 0);

            if (dst == null)
            {
                var msg = String.Format("Failed to create new SDL surface: %s", SDL.SDL_GetError());
                Log.Error(msg);
                throw new NullReferenceException(msg);
            }

            var dstRect = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = width,
                h = height
            };

            SDL.SDL_BlitScaled(Data, IntPtr.Zero, dst, ref dstRect);

            return new Surface(dst);
        }

        protected unsafe Cairo.ImageSurface createCairoSurface()
        {
            var surface = (SDL.SDL_Surface*)Data;

            return new Cairo.ImageSurface(
                surface->pixels, Cairo.Format.ARGB32, surface->w, surface->h, surface->pitch
            );
        }
    }
}
