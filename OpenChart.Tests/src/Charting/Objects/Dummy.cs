using OpenChart.Charting;
using OpenChart.Charting.Objects;

namespace OpenChart.Tests.Charting.Objects
{
    /// <summary>
    /// A dummy class used to represent a BaseObject instance.
    /// </summary>
    class DummyObject : BaseObject
    {
        public DummyObject(KeyIndex key, Beat beat) : base(key, beat) { }
    }

    /// <summary>
    /// A dummy class used to represent a BaseLongObject instance.
    /// </summary>
    class DummyLongObject : BaseLongObject
    {
        public DummyLongObject(KeyIndex key, Beat beat, BeatDuration length) : base(key, beat, length) { }
    }
}
