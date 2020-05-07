using Gtk;
using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.Charting.Properties;
using OpenChart.NoteSkins;
using System.Collections.Generic;

namespace OpenChart.UI.Widgets
{
    /// <summary>
    /// A container widget that represents a single key/column of a chart. Chart objects are added
    /// to the container and then rendered.
    /// </summary>
    public class NoteFieldKey : Fixed
    {
        SortedList<Beat, INoteFieldChartObject> objects;

        /// <summary>
        /// The note field data this widget should display.
        /// </summary>
        readonly NoteFieldData noteFieldData;

        /// <summary>
        /// The key index that this widget represents.
        /// </summary>
        public readonly KeyIndex KeyIndex;

        /// <summary>
        /// Creates a new NoteFieldKey instance.
        /// </summary>
        public NoteFieldKey(NoteFieldData noteFieldData, KeyIndex keyIndex)
        {
            this.noteFieldData = noteFieldData;
            KeyIndex = keyIndex;

            objects = new SortedList<Beat, INoteFieldChartObject>();
        }

        /// <summary>
        /// Adds a chart object to be displayed. This converts the chart object into a note field
        /// widget and adds it.
        /// </summary>
        public void Add(NativeObjects.BaseObject chartObject)
        {
            // Create a widget for the object.
            var noteFieldObject = NoteFieldUtils.CreateWidgetForChartObject(
                noteFieldData,
                chartObject,
                noteFieldData.NoteSkin.Keys[KeyIndex.Value]
            );

            // Add the widget to the container.
            Add(noteFieldObject.GetWidget());
            objects[noteFieldObject.GetChartObject().Beat] = noteFieldObject;
            updateObjectPosition(noteFieldObject);
        }

        /// <summary>
        /// Removes the chart object at the given beat, and returns true if it was removed.
        /// </summary>
        public bool RemoveAtBeat(Beat beat)
        {
            return objects.Remove(beat);
        }

        /// <summary>
        /// Draws the chart object/widgets. This method is overridden because we want to draw
        /// the objects in the correct order, otherwise newer added objects will be rendered on
        /// top of existing objects even if they are at an earlier beat.
        /// </summary>
        protected override bool OnDrawn(Cairo.Context cr)
        {
            foreach (var obj in objects.Values)
            {
                PropagateDraw(obj.GetWidget(), cr);
            }

            return true;
        }

        /// <summary>
        /// Moves a chart object to the correct position.
        /// </summary>
        private void updateObjectPosition(INoteFieldChartObject obj)
        {
            var pos = noteFieldData.GetPosition(obj.GetChartObject().Time);

            if (noteFieldData.CenterObjectsOnBeatLines)
                pos -= obj.GetWidgetCenterOffset();

            Move(obj.GetWidget(), 0, pos);
        }
    }
}
