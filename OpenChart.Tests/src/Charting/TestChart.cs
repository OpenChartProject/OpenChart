using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Objects;
using System;
using System.Collections.Generic;

namespace OpenChart.Tests.Charting
{
    public class TestChart
    {
        Chart chart;

        [SetUp]
        public void SetUp()
        {
            chart = new Chart(4);
        }

        [Test]
        public void Test_Init_4k()
        {
            Assert.AreEqual(4, chart.KeyCount.Value);
            Assert.AreEqual(0, chart.BPMs.Length);
            Assert.AreEqual(chart.KeyCount.Value, chart.Objects.Length);

            for (var i = 0; i < chart.KeyCount.Value; i++)
            {
                Assert.AreEqual(0, chart.Objects[i].Count);
            }
        }

        [TestCase(-1)]
        [TestCase(0)]
        public void Test_KeyCountLessThanOne(int value)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Chart(value));
        }

        [Test]
        public void Test_BPMs()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
            };

            chart.AddBPM(bpms[0]);
            chart.AddBPM(bpms[1]);

            Assert.AreEqual(2, chart.BPMs.Length);
            Assert.AreEqual(bpms[0], chart.BPMs[0]);
            Assert.AreEqual(bpms[1], chart.BPMs[1]);
        }

        [Test]
        public void Test_BPMs_IsCached()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
            };

            chart.AddBPM(bpms[0]);
            chart.AddBPM(bpms[1]);

            Assert.AreSame(chart.BPMs, chart.BPMs);
        }

        [Test]
        public void Test_AddBPM_Null()
        {
            Assert.Throws<ArgumentNullException>(() => chart.AddBPM(null));
        }

        [Test]
        public void Test_AddBPM_FirstBPMNonZeroBeat()
        {
            var bpm = new BPM(100, 1);
            Assert.Throws<ArgumentException>(() => chart.AddBPM(bpm));
        }

        [Test]
        public void Test_AddBPM_FirstBPMZeroBeat()
        {
            var bpm = new BPM(100, 0);

            Assert.DoesNotThrow(() => chart.AddBPM(bpm));
            Assert.AreEqual(1, chart.BPMs.Length);
            Assert.AreEqual(bpm, chart.BPMs[0]);
        }

        [Test]
        public void Test_AddObject_Null()
        {
            Assert.Throws<ArgumentNullException>(() => chart.AddObject(null));
        }

        [Test]
        public void Test_AddObject_KeyOutOfRange()
        {
            var obj = new TapNote(chart.KeyCount.Value, 0);
            Assert.Throws<ArgumentOutOfRangeException>(() => chart.AddObject(obj));
        }

        [Test]
        public void Test_AddObject_KeyInRange()
        {
            for (var i = 0; i < chart.KeyCount.Value; i++)
            {
                var obj = new TapNote(i, 0);
                Assert.DoesNotThrow(() => chart.AddObject(obj));
            }
        }

        [Test]
        public void Test_AddObject_EmptyColumn()
        {
            var obj = new TapNote(0, 0);
            chart.AddObject(obj);

            Assert.AreEqual(1, chart.Objects[0].Count);
            Assert.AreSame(obj, chart.Objects[0].First.Value);
        }

        [Test]
        public void Test_AddObject_ObjectsInOrder()
        {
            var objs = new BaseObject[] {
                new TapNote(0, 0),
                new TapNote(0, 1),
                new TapNote(0, 2)
            };

            chart.AddObject(objs[0]);
            chart.AddObject(objs[1]);
            chart.AddObject(objs[2]);

            Assert.AreEqual(3, chart.Objects[0].Count);
            Assert.AreSame(objs[0], chart.Objects[0].First.Value);
            Assert.AreSame(objs[1], chart.Objects[0].First.Next.Value);
            Assert.AreSame(objs[2], chart.Objects[0].First.Next.Next.Value);
        }

        [Test]
        public void Test_AddObject_ObjectsOutOfOrder()
        {
            var objs = new BaseObject[] {
                new TapNote(0, 2),
                new TapNote(0, 0),
                new TapNote(0, 1)
            };

            chart.AddObject(objs[0]);
            chart.AddObject(objs[1]);
            chart.AddObject(objs[2]);

            Assert.AreEqual(3, chart.Objects[0].Count);
            Assert.AreSame(objs[1], chart.Objects[0].First.Value);
            Assert.AreSame(objs[2], chart.Objects[0].First.Next.Value);
            Assert.AreSame(objs[0], chart.Objects[0].First.Next.Next.Value);
        }

        [Test]
        public void Test_AddObject_AddSameObject()
        {
            var obj = new TapNote(0, 0);
            chart.AddObject(obj);
            Assert.Throws<ChartException>(() => chart.AddObject(obj));
        }

        [Test]
        public void Test_AddObject_TwoObjectsOnSameBeat()
        {
            var objs = new BaseObject[] {
                new TapNote(0, 0),
                new TapNote(0, 0)
            };

            chart.AddObject(objs[0]);
            Assert.Throws<ChartException>(() => chart.AddObject(objs[1]));
        }

        // This is basically replicating Test_AddObject_ObjectsOutOfOrder
        [Test]
        public void Test_AddObjects_SameKeyUnsorted()
        {
            var objs = new BaseObject[] {
                new TapNote(0, 2),
                new TapNote(0, 0),
                new TapNote(0, 1)
            };

            chart.AddObjects(objs);

            Assert.AreEqual(3, chart.Objects[0].Count);
            Assert.AreSame(objs[1], chart.Objects[0].First.Value);
            Assert.AreSame(objs[2], chart.Objects[0].First.Next.Value);
            Assert.AreSame(objs[0], chart.Objects[0].First.Next.Next.Value);
        }

        [Test]
        public void Test_AddObjects_DifferentKeysUnsorted()
        {
            var beats = new double[] { 2, 0, 1, 3 };
            var sortedBeats = new double[] { 0, 1, 2, 3 };

            List<BaseObject> objs = new List<BaseObject>();

            // Create a list of tap notes.
            for (var key = 0; key < chart.KeyCount.Value; key++)
            {
                foreach (var beat in beats)
                {
                    objs.Add(new TapNote(key, beat));
                }
            }

            chart.AddObjects(objs.ToArray());

            // Check they were added in the correct order.
            for (var key = 0; key < chart.KeyCount.Value; key++)
            {
                var col = chart.Objects[key];
                Assert.AreEqual(beats.Length, col.Count);

                var cur = col.First;

                foreach (var beat in sortedBeats)
                {
                    Assert.AreEqual(beat, cur.Value.Beat.Value);
                    Assert.AreEqual(key, cur.Value.Key.Value);
                    cur = cur.Next;
                }
            }
        }
    }
}