using OpenChart.Charting.Objects;
using OpenChart.Charting.Exceptions;
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

            switch (args.Key)
            {
                case SDL_Keycode.SDLK_1:
                    placeNote(e, 0);
                    break;
                case SDL_Keycode.SDLK_2:
                    placeNote(e, 1);
                    break;
                case SDL_Keycode.SDLK_3:
                    placeNote(e, 2);
                    break;
                case SDL_Keycode.SDLK_4:
                    placeNote(e, 3);
                    break;
                case SDL_Keycode.SDLK_DOWN:
                    Settings.Scroll(1);
                    e.Consume();
                    break;
                case SDL_Keycode.SDLK_UP:
                    Settings.Scroll(-1);
                    e.Consume();
                    break;
            }
        }

        protected void placeNote(InputEvent e, KeyIndex keyIndex)
        {
            // Don't try to place more notes if the user is holding the key down.
            if ((e.Args as InputEventFactory.KeyEventArgs).Repeated)
                return;

            var removed = Settings.Chart.Objects[keyIndex.Value].RemoveAtBeat(Settings.ReceptorBeatTime.Beat);

            if (!removed)
            {
                try
                {
                    Settings.Chart.Objects[keyIndex.Value].Add(new TapNote(keyIndex, Settings.ReceptorBeatTime.Beat));
                }
                catch (ObjectOverlapException)
                {
                    // TODO: Try to fix the overlap.
                }
            }

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
