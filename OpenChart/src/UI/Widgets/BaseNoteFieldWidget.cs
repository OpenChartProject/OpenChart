using OpenChart.NoteSkins;
using System;

namespace OpenChart.UI.Widgets
{
    public abstract class BaseNoteFieldWidget : ImageWidget, IKeyWidget, ISkinnedWidget
    {
        int _key;

        /// <summary>
        /// The key the widget is bound to.
        /// </summary>
        public int Key
        {
            get => _key;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Key cannot be negative.");
                }

                if (_key != value)
                {
                    _key = value;
                    OnKeyChanged();
                }
            }
        }

        KeyModeSkin _noteSkin;

        /// <summary>
        /// The note skin the widget is using.
        /// </summary>
        public KeyModeSkin NoteSkin
        {
            get => _noteSkin; set
            {
                if (_noteSkin != value)
                {
                    _noteSkin = value;
                    OnNoteSkinChanged();
                }
            }
        }

        /// <summary>
        /// An event fired when the key the widget is assigned to changes.
        /// </summary>
        public event EventHandler KeyChanged;

        /// <summary>
        /// An event fired when the note skin the widget is using changes.
        /// </summary>
        public event EventHandler NoteSkinChanged;

        public BaseNoteFieldWidget(KeyModeSkin noteSkin, int key)
        {
            Key = key;
            NoteSkin = noteSkin;
        }

        /// <summary>
        /// Fires a KeyChanged event.
        /// </summary>
        protected virtual void OnKeyChanged()
        {
            var handler = KeyChanged;
            handler?.Invoke(this, null);
        }

        /// <summary>
        /// Fires a NoteSkinChanged event.
        /// </summary>
        protected virtual void OnNoteSkinChanged()
        {
            var handler = NoteSkinChanged;
            handler?.Invoke(this, null);
        }
    }
}
