using OpenChart.Charting.Properties;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// The base class for a chart object that occurs on a specific key at a certain beat.
    /// </summary>
    public abstract class BaseObject : IBeatObject, IKeyedObject
    {
        public Beat Beat { get; private set; }

        public KeyIndex KeyIndex { get; private set; }

        public BaseObject(KeyIndex key, Beat beat)
        {
            Beat = beat;
            KeyIndex = key;
        }
    }
}
