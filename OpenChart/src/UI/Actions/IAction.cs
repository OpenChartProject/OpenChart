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
        static string Hotkey { get; }

        /// <summary>
        /// The name of the action. This is the name of the action referred to in
        /// the code, not its display name. Display names are set in <see cref="OpenChart.UI.Actions.MenuModel" />
        /// </summary>
        static string Name { get; }
    }
}
