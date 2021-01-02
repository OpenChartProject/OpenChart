using OpenChart.Charting.Objects;
using static SDL2.SDL;

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
                    handleScroll(e);
                    break;

                case InputEventType.KeyUp:
                    handleKeyPress(e);
                    break;
            }
        }

        protected void handleKeyPress(InputEvent e)
        {
            var args = e.Args as InputEventFactory.KeyEventArgs;
            var keyIndex = -1;

            switch (args.Key)
            {
                case SDL_Keycode.SDLK_1:
                    keyIndex = 0;
                    break;
                case SDL_Keycode.SDLK_2:
                    keyIndex = 1;
                    break;
                case SDL_Keycode.SDLK_3:
                    keyIndex = 2;
                    break;
                case SDL_Keycode.SDLK_4:
                    keyIndex = 3;
                    break;
            }

            if (keyIndex != -1)
            {
                var removed = Settings.Chart.Objects[keyIndex].RemoveAtBeat(Settings.ReceptorBeatTime.Beat);

                if (!removed)
                    Settings.Chart.Objects[keyIndex].Add(new TapNote(keyIndex, Settings.ReceptorBeatTime.Beat));

                e.Consume();
            }
        }

        protected void handleScroll(InputEvent e)
        {
            var args = e.Args as InputEventFactory.ScrolledEventArgs;
            Settings.Scroll(-args.Y);
            e.Consume();
        }
    }
}
