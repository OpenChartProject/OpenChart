namespace OpenChart.UI.Components.NoteField
{
    public class NoteFieldInputHandler : Component
    {
        public readonly NoteFieldSettings Settings;

        public NoteFieldInputHandler(NoteFieldSettings settings)
        {
            Settings = settings;
        }

        public override void ReceiveEvent(InputEvent e)
        {
            switch (e.Type)
            {
                case InputEventType.Scroll:
                    var args = e.Args as InputEventFactory.ScrolledEventArgs;
                    Settings.Scroll(-args.Y);
                    e.Consume();
                    break;
            }
        }
    }
}
