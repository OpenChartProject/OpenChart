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
        /// The key mode skin that is used for the chart objects.
        /// </summary>
        public readonly KeyModeSkin NoteSkin;

        /// <summary>
        /// The key index that this widget represents.
        /// </summary>
        public readonly KeyIndex KeyIndex;

        /// <summary>
        /// The note field this widget is for.
        /// </summary>
        public readonly NoteField NoteField;

        /// <summary>
        /// Creates a new NoteFieldKey instance.
        /// </summary>
        public NoteFieldKey(NoteField noteField, KeyIndex keyIndex, KeyModeSkin noteSkin)
        {
            NoteField = noteField;
            KeyIndex = keyIndex;
            NoteSkin = noteSkin;

            objects = new SortedList<Beat, INoteFieldChartObject>();
        }

        /// <summary>
        /// Adds a chart object to be displayed. This converts the chart object into a note field
        /// widget and adds it.
        /// </summary>
        public void Add(NativeObjects.BaseObject chartObject)
        {
            var noteFieldObject = UIUtils.ChartObjectToWidget(chartObject, NoteSkin.Keys[KeyIndex.Value]);

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
            Move(
                obj.GetWidget(),
                0,
                (int)(obj.GetChartObject().Time.Value * NoteField.TimeSpacing)
            );
        }
    }
}
