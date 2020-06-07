using Gtk;
using OpenChart.UI.Assets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Represents a hold note that is displayed on the note field. A hold note has a head and a body.
    /// </summary>
    public class HoldNote : INoteFieldObject
    {
        public int GetHeight() => Head.GetWidget().AllocatedHeight;

        Charting.Objects.HoldNote chartObject;
        public Charting.Objects.BaseObject GetChartObject() => chartObject;

        VBox container;
        public Widget GetWidget() => container;

        /// <summary>
        /// The body of the hold note.
        /// </summary>
        public HoldNoteBody Body { get; private set; }

        /// <summary>
        /// The head of the hold note.
        /// </summary>
        public HoldNoteHead Head { get; private set; }

        /// <summary>
        /// Creates a new HoldNote instance.
        /// </summary>
        /// <param name="headImage">The image for the head.</param>
        /// <param name="bodyImage">The pattern for the body.</param>
        /// <param name="chartObject">The note object from the chart.</param>
        public HoldNote(
            ImageAsset headImage,
            ImagePattern bodyImage,
            Charting.Objects.HoldNote chartObject
        )
        {
            Body = new HoldNoteBody(bodyImage);
            Head = new HoldNoteHead(headImage);
            this.chartObject = chartObject;

            container = new VBox();
            container.Add(Head.GetWidget());
            container.Add(Body.GetWidget());
        }
    }
}
