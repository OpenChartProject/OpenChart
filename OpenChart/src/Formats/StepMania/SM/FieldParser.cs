using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Formats.StepMania.SM.Exceptions;
using System.Collections.Generic;

namespace OpenChart.Formats.StepMania.SM
{
    /// <summary>
    /// A static class for parsing extracted field data.
    /// </summary>
    public static class FieldParser
    {
        /// <summary>
        /// Parses the display BPM. There are three types of BPM displays:
        ///
        /// - A fixed display that shows a single value: `#DISPLAYBPM:value;`
        /// - A range display that has a lower and upper bound: `#DISPLAYBPM:lower:upper;`
        /// - A random display: `#DISPLAYBPM:*;`
        ///
        /// </summary>
        /// <param name="data">The string data from the DISPLAYBPM field</param>
        public static DisplayBPM ParseDisplayBPM(string data)
        {
            DisplayBPM display = null;

            var parts = data.Split(':');

            if (parts.Length == 2)
            {
                double lower;
                double upper;

                if (
                    !double.TryParse(parts[0].Trim(), out lower) ||
                    !double.TryParse(parts[1].Trim(), out upper)
                )
                    throw new FieldFormatException("Display BPM is not formatted correctly.");

                display = DisplayBPM.NewRangeDisplay(lower, upper);
            }
            else if (parts.Length == 1)
            {
                if (parts[0] == "*")
                    display = DisplayBPM.NewRandomDisplay();
                else
                {
                    double val;

                    if (!double.TryParse(parts[0], out val))
                        throw new FieldFormatException("Display BPM is not formatted correctly.");
                    else
                        display = DisplayBPM.NewFixedDisplay(val);
                }
            }
            else
                throw new FieldFormatException("Display BPM is not formatted correctly.");

            return display;
        }

        /// <summary>
        /// Parse out a list of BPM changes. BPMs are formatted as `Beat=BPM,Beat=BPM,...`
        /// </summary>
        /// <param name="data">The string data from the BPMS field.</param>
        public static List<BPM> ParseBPMList(string data)
        {
            var bpms = new List<BPM>();

            foreach (var bpmChange in data.Split(','))
            {
                // Ignore empty entries.
                if (bpmChange.Trim() == "")
                    continue;

                var parts = bpmChange.Split('=');

                if (parts.Length != 2)
                    throw new FieldFormatException("BPMs are not formatted correctly.");

                double beat;
                double bpm;

                // Try parsing the beat and BPM parts.
                if (!double.TryParse(parts[0].Trim(), out beat) || !double.TryParse(parts[1].Trim(), out bpm))
                    throw new FieldFormatException("BPMs are not formatted correctly.");

                bpms.Add(new BPM(beat, bpm));
            }

            return bpms;
        }

        /// <summary>
        /// Parse out a list of stops. Stops are formatted as `Beat=Seconds,Beat=Seconds,...`
        /// </summary>
        /// <param name="data">The string data from the STOPS field.</param>
        public static List<Stop> ParseStopList(string data)
        {
            var stops = new List<Stop>();

            foreach (var stop in data.Split(','))
            {
                // Ignore empty entries.
                if (stop.Trim() == "")
                    continue;

                var parts = stop.Split('=');

                if (parts.Length != 2)
                    throw new FieldFormatException("Stops are not formatted correctly.");

                double beat;
                double seconds;

                // Try parsing the beat and BPM parts.
                if (!double.TryParse(parts[0].Trim(), out beat) || !double.TryParse(parts[1].Trim(), out seconds))
                    throw new FieldFormatException("Stops are not formatted correctly.");

                stops.Add(new Stop(beat, seconds));
            }

            return stops;
        }

        /// <summary>
        /// Parses all of the header fields for the step file and assigns them to the given
        /// StepFileData instance.
        /// </summary>
        /// <param name="fields">The fields to parse from.</param>
        /// <param name="stepFileData">The object to add the parsed data to.</param>
        public static void ParseHeaders(Fields fields, ref StepFileData stepFileData)
        {
            stepFileData.DisplayData.Banner = fields.GetString("BANNER");
            stepFileData.DisplayData.Background = fields.GetString("BACKGROUND");
            stepFileData.DisplayData.BackgroundChanges = fields.GetString("BGCHANGES");
            stepFileData.DisplayData.ForegroundChanges = fields.GetString("FGCHANGES");
            stepFileData.DisplayData.CDTitle = fields.GetString("CDTITLE");
            stepFileData.DisplayData.Selectable = fields.GetBool("SELECTABLE");
            stepFileData.DisplayData.DisplayBPM = ParseDisplayBPM(fields.GetString("DISPLAYBPM"));

            stepFileData.MetaData.Credit = fields.GetString("CREDIT");

            stepFileData.PlayData.Offset = fields.GetDouble("OFFSET");
            stepFileData.PlayData.BPMs = ParseBPMList(fields.GetString("BPMS"));
            stepFileData.PlayData.Stops = ParseStopList(fields.GetString("STOPS"));

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
        }
    }
}
