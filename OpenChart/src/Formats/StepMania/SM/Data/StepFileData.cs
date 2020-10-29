using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM.Data
{
    /// <summary>
    /// Represents all of the data stored in a .sm file.
    /// </summary>
    public class StepFileData
    {
        /// <summary>
        /// A list of charts for the step file.
        /// </summary>
        public List<Chart> Charts { get; private set; }

        /// <summary>
        /// Display data for the step file.
        /// </summary>
        public DisplayData DisplayData { get; set; }

        /// <summary>
        /// Metadata about the step file.
        /// </summary>
        public StepFileMetaData MetaData { get; set; }

        /// <summary>
        /// Play data for the step file.
        /// </summary>
        public PlayData PlayData { get; set; }

        /// <summary>
        /// The song data.
        /// </summary>
        public SongData SongData { get; set; }

        public StepFileData()
        {
            Charts = new List<Chart>();
            DisplayData = new DisplayData();
            MetaData = new StepFileMetaData();
            PlayData = new PlayData();
            SongData = new SongData();
        }
    }
}
