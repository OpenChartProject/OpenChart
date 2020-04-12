using OpenChart.Charting.Objects;

namespace OpenChart.Tests.Charting.Objects
{
    /// <summary>
    /// A dummy class used to represent a BaseObject instance.
    /// </summary>
    class DummyObject : BaseObject
    {
        public DummyObject(int keyCount, double beat) : base(keyCount, beat) { }
    }

    /// <summary>
    /// A dummy class used to represent a BaseLongObject instance.
    /// </summary>
    class DummyLongObject : BaseLongObject
    {
        public DummyLongObject(int keyCount, double beat, double length) : base(keyCount, beat, length) { }
    }
}