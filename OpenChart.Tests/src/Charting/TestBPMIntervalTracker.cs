using NUnit.Framework;
using OpenChart.Charting;
using OpenChart.Charting.Exceptions;
using OpenChart.Charting.Properties;
using System;

namespace OpenChart.Tests.Charting
{
    public class TestBPMIntervalTracker
    {
        [Test]
        public void Test_Init_CannotBeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BPMIntervalTracker(null));
        }

        [Test]
        public void Test_Init_EmptyList()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);

            Assert.AreSame(list, tracker.ObjectList);
            Assert.IsEmpty(tracker.Intervals);
        }

        [Test]
        public void Test_Init_NonEmptyList()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>(bpms);
            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(3, tracker.Intervals.Length);
            Assert.AreEqual(bpms[0], tracker.Intervals[0].BPM);
            Assert.AreEqual(bpms[1], tracker.Intervals[1].BPM);
            Assert.AreEqual(bpms[2], tracker.Intervals[2].BPM);

            Assert.AreEqual(0, tracker.Intervals[0].Seconds);
            Assert.AreEqual(6.0, tracker.Intervals[1].Seconds);
            Assert.AreEqual(7.5, tracker.Intervals[2].Seconds);
        }

        [Test]
        public void Test_Intervals_ThrowsWhenFirstBPMNotAtBeatZero()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 1));

            var tracker = new BPMIntervalTracker(list);

            try
            {
                var _ = tracker.Intervals;
                Assert.Fail("Expected NoBPMAtBeatZeroException exception to be thrown.");
            }
            catch (NoBPMAtBeatZeroException)
            { }
        }

        [Test]
        public void Test_Intervals_UpdatedWhenListChanged()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);

            list.Add(new BPM(100, 0));
            Assert.AreEqual(1, tracker.Intervals.Length);

            list.Add(new BPM(200, 10));
            Assert.AreEqual(2, tracker.Intervals.Length);

            list.RemoveAtBeat(10);
            Assert.AreEqual(1, tracker.Intervals.Length);

            list.Clear();
            Assert.IsEmpty(tracker.Intervals);
        }
    }
}
