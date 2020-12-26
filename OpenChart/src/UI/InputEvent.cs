namespace OpenChart.UI
{
    public class InputEvent
    {
        public readonly object Args;
        public readonly InputEventType Type;

        public InputEvent(InputEventType type, object args)
        {
            Args = args;
            Type = type;
        }
    }
}
