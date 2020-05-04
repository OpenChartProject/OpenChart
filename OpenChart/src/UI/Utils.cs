using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.NoteSkins;
using OpenChart.UI.Widgets;
using System;

namespace OpenChart.UI
{
    /// <summary>
    /// Static utility class that contains some helpful UI functions.
    /// </summary>
    public static class UIUtils
    {
        /// <summary>
        /// Creates a new widget for the given chart object. The widget matches the type
        /// of the chart object and uses the provided noteskin.
        ///
        /// e.g.:  OpenChart.Charting.Objects.TapNote --> OpenChart.UI.Widgets.TapNote
        ///
        /// </summary>
        /// <param name="chartObject">The chart object.</param>
        /// <param name="noteSkin">The key skin to use when creating the widget.</param>
        public static INoteFieldChartObject CreateWidgetForChartObject(NativeObjects.BaseObject chartObject, NoteSkinKey noteSkin)
        {
            INoteFieldChartObject noteFieldObject;

            if (chartObject is NativeObjects.TapNote tapNote)
                noteFieldObject = new TapNote(noteSkin.TapNote, tapNote);
            else
                throw new Exception("Unknown object type, cannot create widget.");

            return noteFieldObject;
        }
    }
}
