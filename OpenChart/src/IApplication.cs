namespace OpenChart
{
    /// <summary>
    /// An interface for retrieving data about the application.
    /// </summary>
    public interface IApplication
    {
        ApplicationData GetData();
        ApplicationEventBus GetEvents();
        Gtk.Application GetGtk();
        Gtk.Window GetMainWindow();
    }
}
