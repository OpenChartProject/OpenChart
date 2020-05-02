using NUnit.Framework;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Formats.OpenChart.Version0_1.Objects;

namespace OpenChart.Tests.Formats.OpenChart.Data
{
    public class TestBeatRowData
    {
        [Test]
        public void Test_Init()
        {
            var data = new BeatRowData();
            Assert.AreEqual(0, data.Beat.Value);
            Assert.IsEmpty(data.Objects);
        }

        [Test]
        public void Test_Equals()
        {
            Assert.AreEqual(
                new BeatRowData(),
                new BeatRowData()
            );

            Assert.AreEqual(
                new BeatRowData { Beat = 1 },
                new BeatRowData { Beat = 1 }
            );

            Assert.AreEqual(
                new BeatRowData { Objects = new IChartObject[] { new TapNote() } },
                new BeatRowData { Objects = new IChartObject[] { new TapNote() } }
            );

            Assert.AreNotEqual(
                new BeatRowData(),
                null
            );

            Assert.AreNotEqual(
                new BeatRowData { Beat = 1 },
                new BeatRowData()
            );

            Assert.AreNotEqual(
                new BeatRowData { Objects = new IChartObject[] { new TapNote() } },
                new BeatRowData()
            );
        }
    }
}
