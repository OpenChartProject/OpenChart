using System;

namespace OpenChart.UI
{
    public class SurfacePattern : IDisposable
    {
        bool disposed = false;

        public Surface Surface { get; private set; }

        public Cairo.SurfacePattern Pattern { get; private set; }

        public SurfacePattern(Surface surface, Cairo.Extend repeatType = Cairo.Extend.Repeat)
        {
            Surface = surface;
            Pattern = new Cairo.SurfacePattern(surface.CairoSurface);
            Pattern.Extend = repeatType;
        }

        ~SurfacePattern()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                Pattern.Dispose();
                Surface.Dispose();
            }

            Pattern = null;
            Surface = null;
            disposed = true;
        }
    }
}
