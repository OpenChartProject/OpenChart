namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An interface for an undoable action. Undoable actions appear in the Edit menu after they
    /// are executed.
    /// </summary>
    public interface IUndoable
    {
        /// <summary>
        /// Gets the name of the action as a string. This is the text that appears in the UI under
        /// the Edit menu. It doesn't need to be constant.
        /// </summary>
        string GetName();

        /// <summary>
        /// Reverts the action.
        /// </summary>
        void Undo();
    }
}
