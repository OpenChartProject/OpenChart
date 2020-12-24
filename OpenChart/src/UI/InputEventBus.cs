using static SDL2.SDL;
using Serilog;
using System;

namespace OpenChart.UI
{
    /// <summary>
    /// An event bus for handling user input.
    /// </summary>
    public class InputEventBus
    {
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
        /// An event fired when a mouse button is pressed or released.
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> MouseButton;

        /// <summary>
        /// Event args for the MouseMoved event.
        /// </summary>
        public class MouseMovedEventArgs
        {
            public int X, Y;
            public int DeltaX, DeltaY;
        }

        /// <summary>
        /// /// An event fired when the user moves the mouse.
        /// </summary>
        public event EventHandler<MouseMovedEventArgs> MouseMoved;

        /// <summary>
        /// Event args for the Scrolled event.
        /// </summary>
        public class ScrolledEventArgs
        {
            public int X, Y;
        }

        /// <summary>
        /// An event fired when the user scrolls.
        /// </summary>
        public event EventHandler<ScrolledEventArgs> Scrolled;

        /// <summary>
        /// Converts a native SDL event to an application specific event and notifies any subscribers.
        /// </summary>
        public void Dispatch(SDL_Event e)
        {
            switch (e.type)
            {
                case SDL_EventType.SDL_MOUSEWHEEL:
                    {
                        var args = new ScrolledEventArgs
                        {
                            X = e.wheel.x,
                            Y = e.wheel.y
                        };
                        Scrolled?.Invoke(this, args);
                        break;
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
                        MouseButton?.Invoke(this, args);
                        break;
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
                        MouseMoved?.Invoke(this, args);
                        break;
                    }
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
