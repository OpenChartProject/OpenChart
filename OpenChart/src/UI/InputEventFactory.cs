using static SDL2.SDL;
using Serilog;
using System;

namespace OpenChart.UI
{
    /// <summary>
    /// Factory for generating InputEvent objects based on raw SDL events.
    /// </summary>
    public class InputEventFactory
    {
        public class KeyEventArgs
        {
            public SDL_Keycode Key;
            public SDL_Keymod KeyMod;
            public bool Pressed;
        }

        /// <summary>
        /// An enum of different mouse buttons.
        /// </summary>
        public enum MouseButtonType
        {
            Left,
            Middle,
            Right,
            X1,
            X2
        }

        /// <summary>
        /// Event args for the MouseButton event.
        /// </summary>
        public class MouseButtonEventArgs
        {
            public MouseButtonType Button;
            public int Clicks;
            public bool Pressed;
            public int X, Y;
        }

        /// <summary>
        /// Event args for the MouseMoved event.
        /// </summary>
        public class MouseMovedEventArgs
        {
            public int X, Y;
            public int DeltaX, DeltaY;
        }

        /// <summary>
        /// Event args for the Scrolled event.
        /// </summary>
        public class ScrolledEventArgs
        {
            public int X, Y;
        }

        /// <summary>
        /// Converts a native SDL_Event to an InputEvent. Returns null if the SDL_Event isn't supported
        /// by the factory.
        /// </summary>
        public InputEvent CreateEvent(SDL_Event e)
        {
            switch (e.type)
            {
                case SDL_EventType.SDL_KEYDOWN:
                case SDL_EventType.SDL_KEYUP:
                    {
                        var args = new KeyEventArgs
                        {
                            Key = e.key.keysym.sym,
                            KeyMod = e.key.keysym.mod,
                            Pressed = e.key.state == SDL_PRESSED
                        };

                        var type = args.Pressed ? InputEventType.KeyDown : InputEventType.KeyUp;

                        return new InputEvent(type, args);
                    }
                case SDL_EventType.SDL_MOUSEWHEEL:
                    {
                        var args = new ScrolledEventArgs
                        {
                            X = e.wheel.x,
                            Y = e.wheel.y
                        };

                        return new InputEvent(InputEventType.Scroll, args);
                    }

                case SDL_EventType.SDL_MOUSEBUTTONDOWN:
                case SDL_EventType.SDL_MOUSEBUTTONUP:
                    {
                        var args = new MouseButtonEventArgs
                        {
                            Button = convertSdlMouseButton(e.button.button),
                            Clicks = e.button.clicks,
                            Pressed = (e.button.state == SDL_PRESSED),
                            X = e.button.x,
                            Y = e.button.y,
                        };

                        var type = args.Pressed ? InputEventType.MouseDown : InputEventType.MouseUp;

                        return new InputEvent(type, args);
                    }

                case SDL_EventType.SDL_MOUSEMOTION:
                    {
                        var args = new MouseMovedEventArgs
                        {
                            X = e.motion.x,
                            Y = e.motion.y,
                            DeltaX = e.motion.xrel,
                            DeltaY = e.motion.yrel,
                        };

                        return new InputEvent(InputEventType.MouseMove, args);
                    }
                default:
                    return null;
            }
        }

        private MouseButtonType convertSdlMouseButton(uint type)
        {
            switch (type)
            {
                case SDL_BUTTON_LEFT:
                    return MouseButtonType.Left;
                case SDL_BUTTON_MIDDLE:
                    return MouseButtonType.Middle;
                case SDL_BUTTON_RIGHT:
                    return MouseButtonType.Right;
                case SDL_BUTTON_X1:
                    return MouseButtonType.X1;
                case SDL_BUTTON_X2:
                    return MouseButtonType.X2;
            }

            Log.Warning("Unknown SDL button ID: {0}", type);
            return MouseButtonType.Left;
        }
    }
}
