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
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            Thread.CurrentThread.CurrentCulture = culture;
        }
    }
}
