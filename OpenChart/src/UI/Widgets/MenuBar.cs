namespace OpenChart.UI.Widgets
{
    public class MenuBar : Gtk.MenuBar
    {
        public MenuBar(GLib.MenuModel menuModel)
        {
            this.BindModel(menuModel, "app", false);
        }
    }
}
