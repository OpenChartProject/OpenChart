using NUnit.Framework;
using System.Globalization;
using System.Threading;

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

            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
