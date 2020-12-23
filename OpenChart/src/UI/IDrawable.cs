namespace OpenChart.UI
{
    /// <summary>
    /// An interface for an object which can draw.
    /// </summary>
    public interface IDrawable
    {
        void Draw(Cairo.Context ctx);
    }
}
