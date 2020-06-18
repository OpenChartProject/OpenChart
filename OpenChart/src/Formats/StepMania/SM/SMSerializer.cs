using OpenChart.Formats.StepMania.SM.Data;
using System.Collections.Generic;
using System.Text;

namespace OpenChart.Formats.StepMania.SM
{

    /// <summary>
    /// Serializer class for importing .sm files.
    ///
    /// .sm docs: https://github.com/stepmania/stepmania/wiki/sm
    /// </summary>
    public class SMSerializer : IFormatSerializer<StepFileData>
    {
        /// <summary>
        /// Deserializes raw file data into a StepFileData object.
        /// </summary>
        public StepFileData Deserialize(byte[] data)
        {
            var fields = FieldExtractor.Extract(Encoding.UTF8.GetString(data));
            var stepFileData = new StepFileData();

            stepFileData.DisplayData.Banner = fields.GetString("BANNER");
            stepFileData.DisplayData.Background = fields.GetString("BACKGROUND");
            stepFileData.DisplayData.BackgroundChanges = fields.GetString("BGCHANGES");
            stepFileData.DisplayData.ForegroundChanges = fields.GetString("FGCHANGES");
            stepFileData.DisplayData.CDTitle = fields.GetString("CDTITLE");
            stepFileData.DisplayData.Selectable = fields.GetBool("SELECTABLE");

            stepFileData.MetaData.Credit = fields.GetString("CREDIT");

            stepFileData.PlayData.Offset = fields.GetDouble("OFFSET");
            // TODO: BPMs, Stops

            stepFileData.SongData.Genre = fields.GetString("GENRE");
            stepFileData.SongData.LyricsPath = fields.GetString("LYRICSPATH");
            stepFileData.SongData.Music = fields.GetString("MUSIC");
            stepFileData.SongData.SampleLength = fields.GetDouble("SAMPLELENGTH");
            stepFileData.SongData.SampleStart = fields.GetDouble("SAMPLESTART");
            stepFileData.SongData.Subtitle = fields.GetString("SUBTITLE");
            stepFileData.SongData.Title = fields.GetString("TITLE");
            stepFileData.SongData.TransliteratedArtist = fields.GetString("ARTISTTRANSLIT");
            stepFileData.SongData.TransliteratedSubtitle = fields.GetString("SUBTITLETRANSLIT");
            stepFileData.SongData.TransliteratedTitle = fields.GetString("TITLETRANSLIT");


            return stepFileData;
        }

        /// <summary>
        /// Serializes a StepFileData object into the raw .sm data.
        /// </summary>
        public byte[] Serialize(StepFileData obj)
        {
            return null;
        }
    }
}
