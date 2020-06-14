namespace OpenChart.UI.MenuActions
{
    /// <summary>
    /// An interface for an action. These actions are fairly analagous to the Command pattern,
    /// the key difference being that once an action is created it lives for the lifecycle
    /// of the application.
    ///
    /// Action instances are stored in <see cref="OpenChart.Application.ActionDict" /> and can
    /// be retrieved by their name.
    ///
    /// For dynamic actions that need to respond to the state of the application (such as only
    /// allowing the "Close Project" action to fire when a project is actually loaded), actions
    /// should add event listener(s) to <see cref="OpenChart.Application.EventBus" /> and respond
    /// to those events within the action class itself. Keeping all of the action-related code
    /// inside the action classes makes it easy to find later on.
    /// </summary>
    public interface IMenuAction
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
        /// the code, not its display name. Display names are set in <see cref="OpenChart.UI.MenuActions.MenuModel" />
        /// </summary>
        string GetName();
    }
}
