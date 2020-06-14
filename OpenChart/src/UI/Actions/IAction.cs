namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An interface for a general purpose action. These actions differ from menu actions in that
    /// the menu actions are meant to be the UI part whereas classes that implement IAction are
    /// the logical part. Menu actions most likely will end up calling an IAction instance.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Executes the action.
        /// </summary>
        void Run();
    }
}
