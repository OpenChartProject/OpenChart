namespace OpenChart.UI
{
    public interface IComponent
    {
        Cairo.Rectangle Rect { get; }
        void Draw(Cairo.Context ctx);
        void ReceiveEvent();
    }
}
