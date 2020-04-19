using NUnit.Framework;
using OpenChart.Charting;
using System;
using System.Collections.Generic;

namespace OpenChart.Tests.Charting
{
    public class TestBeatObjectList
    {
        [Test]
        public void Test_Add_NullThrowsException()
        {
            var list = new BeatObjectList<BPM>();
            Assert.Throws<ArgumentNullException>(() => list.Add(null));
        }

        [Test]
        public void Test_Add_InsertsObject()
        {
            var list = new BeatObjectList<BPM>();
            var bpm = new BPM(100, 0);

            list.Add(bpm);
            Assert.AreEqual(1, list.Count);

            var array = list.ToArray();
            Assert.AreSame(array[0], bpm);
        }

        [Test]
        public void Test_Add_FiresAddedEvent()
        {
            var list = new BeatObjectList<BPM>();
            var bpm = new BPM(100, 0);
            var calls = 0;

            list.Added += (o, e) =>
            {
                var args = e as ObjectListEventArgs<BPM>;
                Assert.AreSame(bpm, args.Object);
                calls++;
            };

            list.Add(bpm);
            Assert.AreEqual(calls, 1);
        }

        [Test]
        public void Test_Add_ObjectsWithSameBeat()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 0));
            Assert.Throws<ArgumentException>(() => list.Add(new BPM(200, 0)));
        }

        [Test]
        public void Test_AddMultiple_NullThrowsException()
        {
            var list = new BeatObjectList<BPM>();
            Assert.Throws<ArgumentNullException>(() => list.AddMultiple(null));
        }

        [Test]
        public void Test_AddMultiple_ObjectsOutOfOrder()
        {
            var list = new BeatObjectList<BPM>();
            var objects = new BPM[] {
                new BPM(100, 5),
                new BPM(100, 3),
                new BPM(100, 2),
            };

            list.AddMultiple(objects);

            var array = list.ToArray();
            Assert.AreSame(objects[2], array[0]);
            Assert.AreSame(objects[1], array[1]);
            Assert.AreSame(objects[0], array[2]);
        }

        [Test]
        public void Test_AddMultiple_FiresAddedEvents()
        {
            var list = new BeatObjectList<BPM>();
            var objects = new BPM[] {
                new BPM(100, 5),
                new BPM(100, 3),
                new BPM(100, 2),
            };
            var added = new List<BPM>();

            list.Added += (o, e) =>
            {
                var args = e as ObjectListEventArgs<BPM>;
                added.Add(args.Object);
            };

            list.AddMultiple(objects);
            Assert.AreSame(objects[2], added[0]);
            Assert.AreSame(objects[1], added[1]);
            Assert.AreSame(objects[0], added[2]);
        }

        [Test]
        public void Test_Clear_ClearsList()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 0));
            list.Add(new BPM(100, 5));
            list.Clear();

            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Test_Clear_FiresClearedEvent()
        {
            var list = new BeatObjectList<BPM>();

            list.Add(new BPM(100, 0));
            list.Add(new BPM(100, 5));

            var calls = 0;
            list.Cleared += (o, e) => calls++;
            list.Clear();

            Assert.AreEqual(1, calls);
            Assert.AreEqual(0, list.Count);
        }
    }
}
