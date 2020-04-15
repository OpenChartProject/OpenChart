using OpenChart.NoteSkins;

namespace OpenChart.UI.Widgets
{
    public class TapNoteWidget : BaseNoteFieldWidget
    {
        public TapNoteWidget(KeyModeSkin noteSkin, int key) : base(noteSkin, key)
        {
            KeyChanged += delegate { UpdateAppearance(); };
            NoteSkinChanged += delegate { UpdateAppearance(); };

            UpdateAppearance();
        }

        /// <summary>
        /// Updates the appearance of the widget.
        /// </summary>
        protected virtual void UpdateAppearance()
        {
            // Use the tap note image from the noteskin.
            Image = NoteSkin?.Keys[Key]?.TapNote;
        }
    }
}
