using Gtk;
using OpenChart.UI.Assets;
using OpenChart.UI.Widgets;

namespace OpenChart.UI.NoteField.Objects
{
    /// <summary>
    /// Represents a hold note that is displayed on the note field. A hold note has a head and a body.
    /// </summary>
    public class HoldNote : INoteFieldObject
    {
        public int GetHeight() => Head.ImageAsset.Pixbuf.Height;

        Charting.Objects.HoldNote chartObject;
        public Charting.Objects.BaseObject GetChartObject() => chartObject;

        SortedContainer<int> container;
        public Widget GetWidget() => container;

        /// <summary>
        /// The body of the hold note.
        /// </summary>
        public HoldNoteBody Body { get; private set; }

        /// <summary>
        /// The head of the hold note.
        /// </summary>
        public HoldNoteHead Head { get; private set; }

        public NoteFieldSettings NoteFieldSettings { get; private set; }

        /// <summary>
        /// Creates a new HoldNote instance.
        /// </summary>
        /// <param name="headImage">The image for the head.</param>
        /// <param name="bodyImage">The pattern for the body.</param>
        /// <param name="chartObject">The note object from the chart.</param>
        public HoldNote(
            NoteFieldSettings noteFieldSettings,
            ImageAsset headImage,
            ImagePattern bodyImage,
            Charting.Objects.HoldNote chartObject
        )
        {
            NoteFieldSettings = noteFieldSettings;
            Body = new HoldNoteBody(bodyImage);
            Head = new HoldNoteHead(headImage);
            this.chartObject = chartObject;

            container = new SortedContainer<int>();
            container.Put(0, Body.GetWidget(), 0, GetHeight() / 2);
            container.Add(1, Head.GetWidget());

            chartObject.Length.Changed += delegate { UpdateLength(); };

            UpdateLength();
        }

        /// <summary>
        /// Updates the length of the hold note.
        /// </summary>
        public void UpdateLength()
        {
            var start = NoteFieldSettings.BeatToPosition(chartObject.Beat);
            var end = NoteFieldSettings.BeatToPosition(chartObject.EndBeat);

            Body.SetHeight(end - start);
        }
    }
}
