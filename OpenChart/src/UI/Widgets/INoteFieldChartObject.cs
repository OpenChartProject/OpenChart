using OpenChart.Charting.Objects;

namespace OpenChart.UI.Widgets
{
    public interface INoteFieldChartObject
    {
        Gtk.Widget GetWidget();
        BaseObject GetChartObject();
        int GetWidgetCenterOffset();
    }
}
