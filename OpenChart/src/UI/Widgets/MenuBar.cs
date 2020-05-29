namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// A menu bar widget displayed at the top of the window.
    /// </summary>
    public class MenuBar : Gtk.MenuBar
    {
        /// <summary>
        /// Creates a new MenuBar instance.
        /// </summary>
        /// <param name="menuModel">The model the menu should display.</param>
        public MenuBar(GLib.MenuModel menuModel)
        {
            this.BindModel(menuModel, "app", false);
        }
    }
}
