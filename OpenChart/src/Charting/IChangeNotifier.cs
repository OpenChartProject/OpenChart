using System;

namespace OpenChart.Charting
{
    /// <summary>
    /// An interface for objects which fire an event when they are modified.
    /// </summary>
    public interface IChangeNotifier
    {
        event EventHandler Changed;
    }
}
