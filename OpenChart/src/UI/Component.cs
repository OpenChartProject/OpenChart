namespace OpenChart.UI
{
    public abstract class Component
    {
        public Cairo.Rectangle Rect { get; }

        public Component()
        {
            Rect = new Cairo.Rectangle();
        }

        public virtual void Draw(Cairo.Context ctx) { }
        public virtual void ReceiveEvent(InputEvent e) { }
    }
}
