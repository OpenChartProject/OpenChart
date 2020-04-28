using OpenChart.Charting.Objects;

namespace OpenChart.UI.Widgets
{
    public interface INoteFieldObject
    {
        Gtk.Widget GetWidget();
        BaseObject GetChartObject();
    }
}
