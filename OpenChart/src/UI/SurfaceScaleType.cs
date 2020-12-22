namespace OpenChart.UI
{
    /// <summary>
    /// The scale type determines which dimension (width or height) from the surface will be used
    /// as the target for scaling. Scaling maintains the aspect ratio of the surface.
    ///
    /// For example, scaling to 300px with type = Width will resize the surface to 300px wide and
    /// the height is determined by the aspect ratio.
    /// </summary>
    public enum SurfaceScaleType
    {
        Width,
        Height,
    }
}
