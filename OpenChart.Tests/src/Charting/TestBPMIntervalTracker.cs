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
        public void Test_Init_ThrowsIfListIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new BPMIntervalTracker(null));
        }

        [Test]
        public void Test_Init_WithEmptyList()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);

            Assert.AreSame(list, tracker.ObjectList);
            Assert.IsEmpty(tracker.Intervals);
        }

        [Test]
        public void Test_Init_WithNonEmptyList()
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

            Assert.AreEqual(0, tracker.Intervals[0].Time.Value);
            Assert.AreEqual(6.0, tracker.Intervals[1].Time.Value);
            Assert.AreEqual(7.5, tracker.Intervals[2].Time.Value);
        }

        [Test]
        public void Test_Intervals_ThrowsWhenFirstBPMNotAtBeatZero()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 1));

            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<NoBPMAtBeatZeroException>(() => tracker.Intervals.GetValue(0));
        }

        [Test]
        public void Test_Intervals_UpdatesWhenListIsChanged()
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

        [Test]
        public void Test_BeatToTime_ThrowsWhenListIsEmpty()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<Exception>(() => tracker.BeatToTime(0));
        }

        [Test]
        public void Test_BeatToTime_ThrowsWhenIndexIsOutOfRange()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 0));
            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<ArgumentOutOfRangeException>(() => tracker.BeatToTime(0, fromIndex: 1));
        }

        [Test]
        public void Test_BeatToTime_ReturnsZeroForBeatZero()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(99999, 0));
            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(0, tracker.BeatToTime(0).Value);
        }

        [Test]
        public void Test_BeatToTime_ReturnsTimeAtStartOfInterval()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(tracker.Intervals[0].BeatToTime(0), tracker.BeatToTime(0));
            Assert.AreEqual(tracker.Intervals[0].BeatToTime(10), tracker.BeatToTime(10));
            Assert.AreEqual(tracker.Intervals[1].BeatToTime(15), tracker.BeatToTime(15));
        }

        [Test]
        public void Test_BeatToTime_ReturnsTimeInBetweenIntervals()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(tracker.Intervals[0].BeatToTime(5), tracker.BeatToTime(5));
            Assert.AreEqual(tracker.Intervals[1].BeatToTime(12.5), tracker.BeatToTime(12.5));
        }

        [Test]
        public void Test_BeatToTime_ReturnsTimeAfterLastInterval()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(tracker.Intervals[2].BeatToTime(20), tracker.BeatToTime(20));
        }

        [Test]
        public void Test_GetIndexAtTime_ThrowsWhenListIsEmpty()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<Exception>(() => tracker.GetIndexAtTime(0));
        }

        [Test]
        public void Test_GetIndexAtTime_ThrowsWhenIndexIsOutOfRange()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 0));
            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<ArgumentOutOfRangeException>(() => tracker.GetIndexAtTime(0, fromIndex: 1));
        }

        [Test]
        public void Test_GetIndexAtTime_ReturnsZeroForTimeZero()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(99999, 0));
            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(0, tracker.GetIndexAtTime(0));
        }

        [Test]
        public void Test_GetIndexAtTime_ReturnsIndexAtStartOfInterval()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(0, tracker.GetIndexAtTime(tracker.Intervals[0].Time));
            Assert.AreEqual(1, tracker.GetIndexAtTime(tracker.Intervals[1].Time));
            Assert.AreEqual(2, tracker.GetIndexAtTime(tracker.Intervals[2].Time));
        }

        [Test]
        public void Test_GetIndexAtTime_ReturnsIndexInBetweenIntervals()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(0, tracker.GetIndexAtTime((tracker.Intervals[0].Time.Value + tracker.Intervals[1].Time.Value) / 2));
            Assert.AreEqual(1, tracker.GetIndexAtTime((tracker.Intervals[1].Time.Value + tracker.Intervals[2].Time.Value) / 2));
        }

        [Test]
        public void Test_GetIndexAtTime_ReturnsIndexAfterLastInterval()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(2, tracker.GetIndexAtTime(tracker.Intervals[2].Time.Value + 1));
        }

        [Test]
        public void Test_GetBeats_ThrowsWhenListIsEmpty()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);
            var iterator = tracker.GetBeats(0);

            Assert.Throws<Exception>(() =>
            {
                foreach (var _ in iterator) { break; }
            });
        }

        [Test]
        public void Test_GetBeats_ThrowsWhenIndexIsOutOfRange()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 0));
            var tracker = new BPMIntervalTracker(list);
            var iterator = tracker.GetBeats(0, fromIndex: 1);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                foreach (var _ in iterator) { break; }
            });
        }

        [Test]
        public void Test_GetBeats_ReturnsExpectedTime()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 2.5),
                new BPM(300, 4),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);
            var iterator = tracker.GetBeats(0);
            var beat = 0;

            foreach (var time in iterator)
            {
                Assert.AreEqual(tracker.BeatToTime(beat), time);

                if (beat++ > 10)
                    break;
            }
        }

        [Test]
        public void Test_TimeToBeat_ThrowsWhenListIsEmpty()
        {
            var list = new BeatObjectList<BPM>();
            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<Exception>(() => tracker.TimeToBeat(0));
        }

        [Test]
        public void Test_TimeToBeat_ThrowsWhenIndexIsOutOfRange()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(100, 0));
            var tracker = new BPMIntervalTracker(list);

            Assert.Throws<ArgumentOutOfRangeException>(() => tracker.TimeToBeat(0, fromIndex: 1));
        }

        [Test]
        public void Test_TimeToBeat_ReturnsZeroForTimeZero()
        {
            var list = new BeatObjectList<BPM>();
            list.Add(new BPM(99999, 0));
            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(0, tracker.TimeToBeat(0).Value);
        }

        [Test]
        public void Test_TimeToBeat_ReturnsBeatAtStartOfInterval()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(tracker.Intervals[0].BPM.Beat.Value, tracker.TimeToBeat(tracker.Intervals[0].Time).Value);
            Assert.AreEqual(tracker.Intervals[1].BPM.Beat.Value, tracker.TimeToBeat(tracker.Intervals[1].Time).Value);
            Assert.AreEqual(tracker.Intervals[2].BPM.Beat.Value, tracker.TimeToBeat(tracker.Intervals[2].Time).Value);
        }

        [Test]
        public void Test_TimeToBeat_ReturnsBeatInBetweenIntervals()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(tracker.Intervals[0].TimeToBeat(4).Value, tracker.TimeToBeat(4).Value);
            Assert.AreEqual(tracker.Intervals[1].TimeToBeat(7).Value, tracker.TimeToBeat(7).Value);
            Assert.AreEqual(tracker.Intervals[2].TimeToBeat(12.5).Value, tracker.TimeToBeat(12.5).Value);
        }

        [Test]
        public void Test_TimeToBeat_ReturnsBeatAfterLastInterval()
        {
            var bpms = new BPM[] {
                new BPM(100, 0),
                new BPM(200, 10),
                new BPM(300, 15),
            };

            var list = new BeatObjectList<BPM>();
            list.Add(bpms[0]);
            list.Add(bpms[1]);
            list.Add(bpms[2]);

            var tracker = new BPMIntervalTracker(list);

            Assert.AreEqual(tracker.Intervals[2].TimeToBeat(20).Value, tracker.TimeToBeat(20).Value);
        }
    }
}
