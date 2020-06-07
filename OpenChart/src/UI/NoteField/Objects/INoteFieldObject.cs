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
    }
}
