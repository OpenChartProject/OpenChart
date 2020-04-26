using OpenChart.Charting.Exceptions;
using OpenChart.Charting.Properties;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// The base class for a chart object which has a length or duration associated with it.
    /// </summary>
    public abstract class BaseLongObject : BaseObject, IPlacementValidator, IBeatDurationObject
    {
        public BeatDuration BeatDuration { get; private set; }

        public BaseLongObject(KeyIndex key, Beat beat, BeatDuration length) : base(key, beat)
        {
            BeatDuration = length;
        }

        /// <summary>
        /// Checks if the object overlaps with another object. Throws an exception if it does.
        /// </summary>
        public void ValidatePlacement(IBeatObject prev, IBeatObject next)
        {
            // Check if the previous object overlaps with this one.
            if (prev is IPlacementValidator validatable)
                validatable.ValidatePlacement(null, this);

            if (next != null && next.Beat.Value <= (Beat.Value + BeatDuration.Value))
                throw new ObjectOverlapException();
        }
    }
}
