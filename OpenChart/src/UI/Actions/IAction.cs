namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An interface for an action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// The GLib/Gtk action object.
        /// </summary>
        GLib.IAction Action { get; }

        /// <summary>
        /// The hotkey for the action.
        /// </summary>
        string GetHotkey();

        /// <summary>
        /// The name of the action. This is the name of the action referred to in
        /// the code, not its display name. Display names are set in <see cref="OpenChart.UI.Actions.MenuModel" />
        /// </summary>
        string GetName();

        /// <summary>
        /// Sets whether the action is enabled or not.
        /// </summary>
        void SetEnabled(bool enabled);
    }
}
