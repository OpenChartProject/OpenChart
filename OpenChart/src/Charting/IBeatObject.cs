using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// An interface for an object which occurs on a particular beat.
    /// </summary>
    public interface IBeatObject
    {
        Beat Beat { get; }

        /// <summary>
        /// This method is used by beat objects that are put into a list, sorted by beats.
        /// This is called right before insertion, with the previous and next objects in the list.
        /// These objects can be null.
        ///
        /// If there is a reason that this object cannot be inserted between prev and next,
        /// it should raise a `ChartException` with a reason why.
        /// </summary>
        void CheckValid(IBeatObject prev, IBeatObject next) { }
    }
}
