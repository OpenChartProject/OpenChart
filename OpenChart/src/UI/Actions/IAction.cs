namespace OpenChart.UI.Actions
{
    public interface IAction
    {
        GLib.IAction Action { get; }
        static string Hotkey { get; }
        static string Name { get; }
    }
}
