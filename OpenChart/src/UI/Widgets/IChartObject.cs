using OpenChart.Charting.Objects;

namespace OpenChart.UI.Widgets
{
    public interface IChartObject
    {
        Gtk.Widget GetWidget();
        BaseObject GetChartObject();
    }
}
