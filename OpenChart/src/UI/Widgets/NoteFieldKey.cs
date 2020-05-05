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
        readonly NoteFieldData NoteFieldData;

        /// <summary>
        /// The key index that this widget represents.
        /// </summary>
        public readonly KeyIndex KeyIndex;

        /// <summary>
        /// When set to true, objects are offset so that the center of the object is considered
        /// the origin. When false, the origin is considered the top of the object.
        /// </summary>
        public readonly bool CenterObjectsOnBeatLines;

        /// <summary>
        /// Creates a new NoteFieldKey instance.
        /// </summary>
        public NoteFieldKey(
            NoteFieldData noteFieldData,
            KeyIndex keyIndex,
            bool centerObjectsOnBeatLines
        )
        {
            CenterObjectsOnBeatLines = centerObjectsOnBeatLines;
            NoteFieldData = noteFieldData;
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
            var noteFieldObject = UIUtils.CreateWidgetForChartObject(
                NoteFieldData,
                chartObject,
                NoteFieldData.NoteSkin.Keys[KeyIndex.Value]
            );

            // Add the widget to the container.
            Add(noteFieldObject.GetWidget());
            objects[noteFieldObject.GetChartObject().Beat] = noteFieldObject;
            updateObjectPosition(noteFieldObject);
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
            var pos = NoteFieldData.GetPosition(obj.GetChartObject().Time);

            if (CenterObjectsOnBeatLines)
                pos -= obj.GetWidgetCenterOffset();

            Move(obj.GetWidget(), 0, pos);
        }
    }
}
