using NUnit.Framework;
using OpenChart.Charting;
using System;

namespace OpenChart.Tests.Charting
{
    public class TestChartUtils
    {
        [Test]
        public void Test_BeatToSeconds_Empty()
        {
            Assert.Throws<ArgumentException>(
                () => ChartUtils.BeatToSeconds(new BPM[0], 0)
            );
        }

        [TestCase(-1)]
        [TestCase(-0.0001)]
        public void Test_BeatToSeconds_NegativeBeat(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => ChartUtils.BeatToSeconds(new BPM[1], value)
            );
        }

        [Test]
        public void Test_BeatToSeconds_ZeroBeat()
        {
            Assert.AreEqual(0, ChartUtils.BeatToSeconds(new BPM[1], 0));
        }

        [Test]
        public void Test_BeatToSeconds_AfterLastBPMChange()
        {
            // 60BPM @ beat 60 = 60 seconds
            Assert.AreEqual(
                60,
                ChartUtils.BeatToSeconds(new BPM[] { new BPM(60, 0) }, 60)
            );

            // 120BPM @ beat 60 = 30 seconds
            Assert.AreEqual(
                30,
                ChartUtils.BeatToSeconds(new BPM[] { new BPM(120, 0) }, 60)
            );

            // 60BPM -> 120BPM @ beat 60 = 45 seconds
            Assert.AreEqual(
                45,
                ChartUtils.BeatToSeconds(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(120, 30)
                    },
                    60
                )
            );
        }

        [Test]
        public void Test_BeatToSeconds_OnBPMChange()
        {
            // 60BPM @ beat 30 = 30 seconds
            Assert.AreEqual(
                30,
                ChartUtils.BeatToSeconds(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(9999, 30)
                    },
                    30
                )
            );
        }

        [Test]
        [DefaultFloatingPointTolerance(0.1)]
        public void Test_BeatToSeconds_BetweenBPMChange()
        {
            // 60BPM @ beat 15 = 15 seconds
            Assert.AreEqual(
                15,
                ChartUtils.BeatToSeconds(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(9999, 30)
                    },
                    15
                )
            );

            // 60BPM -> 120BPM @ beat 60 = 45 seconds
            Assert.AreEqual(
                45,
                ChartUtils.BeatToSeconds(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(120, 30),
                        new BPM(9999, 60)
                    },
                    60
                )
            );

            // I'm not commenting this one
            Assert.AreEqual(
                10.917,
                ChartUtils.BeatToSeconds(
                    new BPM[]
                    {
                        new BPM(127.4, 0),
                        new BPM(186, 12.425),
                        new BPM(300, 21)
                    },
                    32.5
                )
            );
        }

        [Test]
        public void Test_SecondsToBeat_Empty()
        {
            Assert.Throws<ArgumentException>(
                () => ChartUtils.SecondsToBeat(new BPM[0], 0)
            );
        }

        [TestCase(-1)]
        [TestCase(-0.0001)]
        public void Test_SecondsToBeat_NegativeSeconds(double value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => ChartUtils.SecondsToBeat(new BPM[1], value)
            );
        }

        [Test]
        public void Test_SecondsToBeat_ZeroSeconds()
        {
            Assert.AreEqual(0, ChartUtils.SecondsToBeat(new BPM[1], 0));
        }

        [Test]
        public void Test_SecondsToBeat_AfterLastBPMChange()
        {
            // 60BPM for 60 seconds = beat 60
            Assert.AreEqual(
                60,
                ChartUtils.SecondsToBeat(new BPM[] { new BPM(60, 0) }, 60)
            );

            // 120BPM for 30 seconds = beat 60
            Assert.AreEqual(
                60,
                ChartUtils.SecondsToBeat(new BPM[] { new BPM(120, 0) }, 30)
            );

            // 60BPM -> 120BPM for 45 seconds = beat 60
            Assert.AreEqual(
                60,
                ChartUtils.SecondsToBeat(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(120, 30)
                    },
                    45
                )
            );
        }

        [Test]
        public void Test_SecondsToBeat_OnBPMChange()
        {
            // 60BPM @ for 30 seconds = beat 30
            Assert.AreEqual(
                30,
                ChartUtils.SecondsToBeat(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(9999, 30)
                    },
                    30
                )
            );
        }

        [Test]
        [DefaultFloatingPointTolerance(0.1)]
        public void Test_SecondsToBeat_BetweenBPMChange()
        {
            // 60BPM @ for 15 seconds = beat 15
            Assert.AreEqual(
                15,
                ChartUtils.SecondsToBeat(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(9999, 30)
                    },
                    15
                )
            );

            // 60BPM -> 120BPM for 45 seconds = beat 60
            Assert.AreEqual(
                60,
                ChartUtils.SecondsToBeat(
                    new BPM[]
                    {
                        new BPM(60, 0),
                        new BPM(120, 30),
                        new BPM(9999, 60)
                    },
                    45
                )
            );

            // I'm not commenting this one
            Assert.AreEqual(
                32.5,
                ChartUtils.SecondsToBeat(
                    new BPM[]
                    {
                        new BPM(127.4, 0),
                        new BPM(186, 12.425),
                        new BPM(300, 21)
                    },
                    10.917
                )
            );
        }
    }
}