using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.NoteSkins;
using OpenChart.UI.Widgets;
using System;

namespace OpenChart.UI
{
    /// <summary>
    /// Static utility class that contains some helpful UI functions.
    /// </summary>
    public static class NoteFieldUtils
    {
        /// <summary>
        /// Creates a new widget for the given chart object. The widget matches the type
        /// of the chart object and uses the provided noteskin.
        ///
        /// e.g.:  OpenChart.Charting.Objects.TapNote --> OpenChart.UI.Widgets.TapNote
        ///
        /// </summary>
        /// <param name="noteFieldData">The note field data this widget is being added to.</param>
        /// <param name="chartObject">The chart object.</param>
        /// <param name="noteSkin">The key skin to use when creating the widget.</param>
        public static INoteFieldChartObject CreateWidgetForChartObject(
            NoteFieldData noteFieldData,
            NativeObjects.BaseObject chartObject,
            NoteSkinKey noteSkin
        )
        {
            INoteFieldChartObject noteFieldObject;

            if (chartObject is NativeObjects.TapNote tapNote)
                noteFieldObject = new TapNote(noteSkin.TapNote, tapNote);
            else if (chartObject is NativeObjects.HoldNote holdNote)
                noteFieldObject = new HoldNote(noteFieldData, noteSkin.HoldNote, noteSkin.HoldNoteBody, holdNote);
            else
                throw new Exception("Unknown object type, cannot create widget.");

            return noteFieldObject;
        }
    }
}
