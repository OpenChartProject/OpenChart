using OpenChart.Charting.Exceptions;
using OpenChart.Charting.Properties;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// The base class for a chart object which has a length or duration associated with it.
    /// </summary>
    public abstract class BaseLongObject : BaseObject, IBeatDurationObject
    {
        /// <summary>
        /// The length of the object (in beats).
        /// </summary>
        public BeatDuration Length { get; private set; }

        /// <summary>
        /// The beat this object ends on.
        /// </summary>
        public Beat EndBeat => Beat.Value + Length.Value;

        public BaseLongObject(KeyIndex key, Beat beat, BeatDuration length) : base(key, beat)
        {
            Length = length;
        }

        /// <summary>
        /// Checks if the object overlaps with another object. Throws an exception if it does.
        /// </summary>
        public override void ValidatePlacement(IBeatObject prev, IBeatObject next)
        {
            // Check if the previous object overlaps with this one.
            if (prev is BaseObject prevObj)
                prevObj.ValidatePlacement(null, this);

            // Check if this overlaps with the next object.
            if (next != null && next.Beat.Value <= (Beat.Value + Length.Value))
                throw new ObjectOverlapException();
        }

        /// <summary>
        /// Sets the length of the object.
        /// </summary>
        public void SetLength(BeatDuration length)
        {
            Length = length;
        }
    }
}
