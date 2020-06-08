namespace OpenChart.UI.NoteField
{
    /// <summary>
    /// A wrapper for the note field which adds extra functionality like scrolling.
    /// </summary>
    public class NoteFieldView : IWidget
    {
        /// <summary>
        /// The note field widget.
        /// </summary>
        public NoteField NoteField { get; private set; }

        public Scrollable Container { get; private set; }

        /// <summary>
        /// Returns the full note field view. This restricts the viewport of the note field and
        /// allows it to be scrolled.
        /// </summary>
        public Gtk.Widget GetWidget() => Container.GetWidget();

        /// <summary>
        /// Creates a new NoteFieldView instance.
        /// </summary>
        /// <param name="noteField">The note field widget.</param>
        public NoteFieldView(NoteField noteField)
        {
            NoteField = noteField;
            Container = new Scrollable(NoteField.GetWidget(), 30, 30);
            Container.Move(0, 100);
        }

        /// <summary>
        /// Centers the note field in the view.
        /// </summary>
        public void CenterNoteField()
        {
            var x = Container.GetWidget().AllocatedWidth - NoteField.NoteFieldSettings.NoteFieldWidth;
            Container.Move(x / 2, Container.Y);
        }
    }
}
