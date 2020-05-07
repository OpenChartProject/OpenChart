using NUnit.Framework;

namespace OpenChart.Tests
{
    [SetUpFixture]
    public class Init
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Initialize Gtk for UI tests.
            Gtk.Application.Init();
        }
    }
}
