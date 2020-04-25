using OpenChart.Charting.Properties;

namespace OpenChart.Charting.Objects
{
    /// <summary>
    /// An interface for a beat object that has constraints regarding where it can be
    /// placed inside a BeatObjectList.
    /// </summary>
    public interface IPlacementValidator
    {
        /// <summary>
        /// This method is used by beat objects that are put into a list, sorted by beats.
        /// This is called right before insertion, with the previous and next objects in the list.
        /// These objects can be null.
        ///
        /// If there is a reason that this object cannot be inserted between prev and next,
        /// it should raise a `ChartException` with a reason why.
        /// </summary>
        void ValidatePlacement(IBeatObject prev, IBeatObject next) { }
    }
}
