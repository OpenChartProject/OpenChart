namespace OpenChart.UI
{
    public class InputEvent
    {
        public readonly object Args;
        public readonly InputEventType Type;
        public bool Consumed { get; private set; }

        public InputEvent(InputEventType type, object args)
        {
            Args = args;
            Type = type;
            Consumed = false;
        }

        public void Consume()
        {
            Consumed = true;
        }
    }
}
