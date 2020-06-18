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

            stepFileData.DisplayData.Banner = getString(fields, "BANNER");
            stepFileData.DisplayData.Background = getString(fields, "BACKGROUND");
            stepFileData.DisplayData.BackgroundChanges = getString(fields, "BGCHANGES");
            stepFileData.DisplayData.ForegroundChanges = getString(fields, "FGCHANGES");
            stepFileData.DisplayData.CDTitle = getString(fields, "CDTITLE");
            stepFileData.DisplayData.Selectable = getBool(fields, "SELECTABLE");

            stepFileData.MetaData.Credit = getString(fields, "CREDIT");

            stepFileData.PlayData.Offset = getFloat(fields, "OFFSET");
            // TODO: BPMs, Stops

            stepFileData.SongData.Genre = getString(fields, "GENRE");
            stepFileData.SongData.LyricsPath = getString(fields, "LYRICSPATH");
            stepFileData.SongData.Music = getString(fields, "MUSIC");
            stepFileData.SongData.SampleLength = getFloat(fields, "SAMPLELENGTH");
            stepFileData.SongData.SampleStart = getFloat(fields, "SAMPLESTART");
            stepFileData.SongData.Subtitle = getString(fields, "SUBTITLE");
            stepFileData.SongData.Title = getString(fields, "TITLE");
            stepFileData.SongData.TransliteratedArtist = getString(fields, "ARTISTTRANSLIT");
            stepFileData.SongData.TransliteratedSubtitle = getString(fields, "SUBTITLETRANSLIT");
            stepFileData.SongData.TransliteratedTitle = getString(fields, "TITLETRANSLIT");


            return stepFileData;
        }

        /// <summary>
        /// Serializes a StepFileData object into the raw .sm data.
        /// </summary>
        public byte[] Serialize(StepFileData obj)
        {
            return null;
        }

        private bool getBool(Dictionary<string, string> dict, string key, bool defaultValue = false)
        {
            try
            {
                var val = dict[key];

                return val == "yes" || val == "1";
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        private float getFloat(Dictionary<string, string> dict, string key, float defaultValue = 0)
        {
            try
            {
                return float.Parse(dict[key]);
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        private int getInt(Dictionary<string, string> dict, string key, int defaultValue = 0)
        {
            try
            {
                return int.Parse(dict[key]);
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        private string getString(Dictionary<string, string> dict, string key, string defaultValue = "")
        {
            try
            {
                return dict[key];
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }
    }
}
