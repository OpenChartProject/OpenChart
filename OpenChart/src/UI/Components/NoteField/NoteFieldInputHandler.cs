using OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
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

                case InputEventType.KeyDown:
                    handleKeyPress(e);
                    break;
            }
        }

        protected void handleKeyPress(InputEvent e)
        {
            var args = e.Args as InputEventFactory.KeyEventArgs;

            if (args.Repeated)
                return;

            switch (args.Key)
            {
                case SDL_Keycode.SDLK_1:
                    handlePlaceNote(e, 0);
                    break;
                case SDL_Keycode.SDLK_2:
                    handlePlaceNote(e, 1);
                    break;
                case SDL_Keycode.SDLK_3:
                    handlePlaceNote(e, 2);
                    break;
                case SDL_Keycode.SDLK_4:
                    handlePlaceNote(e, 3);
                    break;
                case SDL_Keycode.SDLK_DOWN:
                    Settings.Scroll(1);
                    break;
                case SDL_Keycode.SDLK_UP:
                    Settings.Scroll(-1);
                    break;
            }
        }

        protected void handlePlaceNote(InputEvent e, KeyIndex keyIndex)
        {
            var removed = Settings.Chart.Objects[keyIndex.Value].RemoveAtBeat(Settings.ReceptorBeatTime.Beat);

            if (!removed)
                Settings.Chart.Objects[keyIndex.Value].Add(new TapNote(keyIndex, Settings.ReceptorBeatTime.Beat));

            e.Consume();
        }

        protected void handleScroll(InputEvent e)
        {
            var args = e.Args as InputEventFactory.ScrolledEventArgs;
            Settings.Scroll(-args.Y);
            e.Consume();
        }
    }
}
