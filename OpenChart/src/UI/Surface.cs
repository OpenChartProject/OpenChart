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

        public int Width => CairoSurface.Width;
        public int Height => CairoSurface.Height;

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
        /// Loads an image at the given path. Returns a new Surface if successful, otherwise null.
        /// </summary>
        /// <param name="path">The path to an image</param>
        public static Surface LoadFromFile(string path)
        {
            var data = SDL_image.IMG_Load(path);

            if (data == IntPtr.Zero)
            {
                var msg = String.Format("Failed to load image: %s", SDL_image.IMG_GetError());
                Log.Warning(msg);
                return null;
            }

            return new Surface(data);
        }

        /// <summary>
        /// Creates and returns a new Surface with the given width and height. The pixels from this
        /// Surface are scaled and rendered to the new Surface.
        /// </summary>
        public Surface Resize(int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                var msg = String.Format("Resize dimensions must be positive (%d, %d)", width, height);
                Log.Error(msg);
                throw new ArgumentOutOfRangeException(msg);
            }

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

        /// <summary>
        /// Util method for resizing a surface while maintaining its aspect ratio. This is useful
        /// for resizing a surface to fit into a specific width or height. Returns a new Surface instance.
        /// </summary>
        /// <param name="value">The size to scale to, in pixels.</param>
        /// <param name="scaleType">The dimension to target for the scaling.</param>
        public Surface ScaleTo(int value, SurfaceScaleType scaleType)
        {
            if (value <= 0)
            {
                var msg = String.Format("ScaleTo value must be a positive number (%d)", value);
                Log.Error(msg);
                throw new ArgumentOutOfRangeException(msg);
            }

            var aspectRatio = (float)Width / Height;

            if (scaleType == SurfaceScaleType.Height)
                return Resize((int)Math.Round(value * aspectRatio), value);
            else if (scaleType == SurfaceScaleType.Width)
                return Resize(value, (int)Math.Round(value / aspectRatio));

            return null;
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