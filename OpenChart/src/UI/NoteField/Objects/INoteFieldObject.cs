using OpenChart.Charting.Objects;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// An interface for a chart object that is displayed on the note field.
    /// </summary>
    public interface INoteFieldObject : IWidget
    {
        /// <summary>
        /// Gets the underlying chart object instance.
        /// </summary>
        BaseObject GetChartObject();

        /// <summary>
        /// Gets the height of the object, in pixels. This is used for aligning the object along
        /// beat lines. For hold notes this is the height of the head, not the body.
        /// </summary>
        int GetHeight();
    }
}
