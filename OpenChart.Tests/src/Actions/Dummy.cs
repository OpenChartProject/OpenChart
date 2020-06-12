using NUnit.Framework;

namespace OpenChart.Tests.Actions
{
    class DummyApp : IApplication
    {
        public ApplicationData AppData;
        public ApplicationData GetData() => AppData;

        public ApplicationEventBus Events;
        public ApplicationEventBus GetEvents() => Events;

        public Gtk.Application GetGtk() => null;

        public Gtk.Window GetMainWindow() => null;

        public DummyApp()
        {
            AppData = new ApplicationData("");
            Events = new ApplicationEventBus(AppData);
        }
    }
}
