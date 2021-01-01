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
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
    }
}
