using System;

namespace OpenChart.UI
{
    /// <summary>
    /// A surface which is repeatable. Used by Cairo.
    /// </summary>
    public class SurfacePattern : IDisposable
    {
        bool disposed = false;

        /// <summary>
        /// The surface this pattern uses.
        /// </summary>
        public Surface Surface { get; private set; }

        /// <summary>
        /// The pattern for use with Cairo.
        /// </summary>
        public Cairo.SurfacePattern Pattern { get; private set; }

        /// <summary>
        /// Creates a new SurfacePattern instance.
        /// </summary>
        /// <param name="surface">The surface to use as a pattern.</param>
        /// <param name="repeatType">How the pattern should be painted.</param>
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

        /// <summary>
        /// Cleans up the pattern. This will NOT dispose the Surface object used by this pattern.
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
                Pattern.Dispose();

            Pattern = null;
            disposed = true;
        }
    }
}
