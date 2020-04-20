using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Properties;
using System;
using System.Collections.Generic;

namespace OpenChart.Tests.Charting
{
    public class TestBeatObjectList
    {
        [Test]
        public void Test_Add_CantBeNull()
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
        public void Test_AddMultiple_CantBeNull()
        {
            var list = new BeatObjectList<BPM>();
            Assert.Throws<ArgumentNullException>(() => list.AddMultiple(null));
        }

        [Test]
        public void Test_AddMultiple_ContainsNull()
        {
            var list = new BeatObjectList<BPM>();
            var objects = new BPM[] {
                new BPM(100, 5),
                null,
                new BPM(100, 2),
            };

            Assert.Throws<ArgumentNullException>(() => list.AddMultiple(objects));
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
            list.Cleared += delegate { calls++; };
            list.Clear();

            Assert.AreEqual(1, calls);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Test_Contains_NullReturnsFalse()
        {
            var list = new BeatObjectList<BPM>();
            Assert.IsFalse(list.Contains(null));
        }

        [Test]
        public void Test_Contains_ObjectExists()
        {
            var list = new BeatObjectList<BPM>();
            var objects = new BPM[] {
                new BPM(100, 2),
                new BPM(100, 3),
                new BPM(100, 5),
            };

            list.AddMultiple(objects);

            foreach (var o in objects)
            {
                Assert.IsTrue(list.Contains(o));
            }
        }

        [Test]
        public void Test_Contains_ObjectDoesNotExist()
        {
            var list = new BeatObjectList<BPM>();
            var objects = new BPM[] {
                new BPM(100, 2),
                new BPM(100, 3),
                new BPM(100, 5),
            };

            list.AddMultiple(objects);

            // New object isn't in the list.
            Assert.IsFalse(list.Contains(new BPM(123, 45)));

            // Copy of the first element added. Tests that it's checking the reference
            // and not the value.
            Assert.IsFalse(list.Contains(new BPM(100, 2)));
        }

        [Test]
        public void Test_Remove_CantBeNull()
        {
            var list = new BeatObjectList<BPM>();
            Assert.Throws<ArgumentNullException>(() => list.Remove(null));
        }

        [Test]
        public void Test_Remove_ObjectDoesNotExist()
        {
            var list = new BeatObjectList<BPM>();
            var bpm = new BPM(100, 0);

            Assert.IsFalse(list.Remove(bpm));
        }

        [Test]
        public void Test_Remove_ObjectExists()
        {
            var list = new BeatObjectList<BPM>();
            var bpm = new BPM(100, 0);

            list.Add(bpm);
            Assert.IsTrue(list.Remove(bpm));
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public void Test_Remove_FiresRemovedEvent()
        {
            var list = new BeatObjectList<BPM>();
            var removedList = new List<BPM>();
            var bpm = new BPM(100, 0);

            list.Add(bpm);
            list.Removed += (o, e) =>
            {
                var args = e as ObjectListEventArgs<BPM>;
                removedList.Add(args.Object);
            };

            list.Remove(new BPM(100, 0));

            // Verify the event doesn't fire when an equivalent but different object is removed.
            Assert.IsEmpty(removedList);

            list.Remove(bpm);

            Assert.AreEqual(1, removedList.Count);
            Assert.AreSame(bpm, removedList[0]);
        }

        [Test]
        public void Test_RemoveAtBeat_CantBeNull()
        {
            var list = new BeatObjectList<BPM>();
            Assert.Throws<ArgumentNullException>(() => list.RemoveAtBeat(null));
        }

        [Test]
        public void Test_RemoveAtBeat_ObjectDoesNotExist()
        {
            var list = new BeatObjectList<BPM>();
            Assert.IsFalse(list.RemoveAtBeat(0));
        }

        [Test]
        public void Test_RemoveAtBeat_ObjectExists()
        {
            var list = new BeatObjectList<BPM>();
            var bpm = new BPM(100, 0);

            list.Add(bpm);

            Assert.IsTrue(list.RemoveAtBeat(bpm.Beat));
            Assert.AreEqual(0, list.Count);
            Assert.IsFalse(list.RemoveAtBeat(bpm.Beat));
        }

        [Test]
        public void Test_RemoveAtBeat_FiresRemovedEvent()
        {
            var list = new BeatObjectList<BPM>();
            var removedList = new List<BPM>();
            var bpm = new BPM(100, 0);

            list.Add(bpm);
            list.Removed += (o, e) =>
            {
                var args = e as ObjectListEventArgs<BPM>;
                removedList.Add(args.Object);
            };

            list.RemoveAtBeat(bpm.Beat.Value + 100);

            // Verify the event wasn't fired when no object is removed.
            Assert.IsEmpty(removedList);

            list.RemoveAtBeat(bpm.Beat);

            Assert.AreEqual(1, removedList.Count);
            Assert.AreSame(bpm, removedList[0]);
        }

        [Test]
        public void Test_ToArray_EmptyList()
        {
            var list = new BeatObjectList<BPM>();
            Assert.IsEmpty(list.ToArray());
        }

        [Test]
        public void Test_ToArray_NonEmptyList()
        {
            var list = new BeatObjectList<BPM>();
            var objects = new BPM[] {
                new BPM(100, 2),
                new BPM(100, 3),
                new BPM(100, 5),
            };

            list.AddMultiple(objects);

            var array = list.ToArray();

            Assert.AreEqual(objects.Length, array.Length);
            Assert.AreSame(objects[0], array[0]);
            Assert.AreSame(objects[1], array[1]);
            Assert.AreSame(objects[2], array[2]);
        }
    }
}
