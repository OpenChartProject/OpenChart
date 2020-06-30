using OpenChart.Formats.StepMania.SM.Data;
using OpenChart.Formats.StepMania.SM.Enums;
using OpenChart.Formats.StepMania.SM.Exceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
                    throw new FieldFormatException("Display BPM is not formatted correctly (not a valid number).");

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
                        throw new FieldFormatException("Display BPM is not formatted correctly (not a valid number).");
                    else
                        display = DisplayBPM.NewFixedDisplay(val);
                }
            }
            else
                throw new FieldFormatException("Display BPM is not formatted correctly (expected one or two arguments).");

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
                    throw new FieldFormatException("BPMs are not formatted correctly (expected a beat=BPM pair).");

                double beat;
                double bpm;

                // Try parsing the beat and BPM parts.
                if (!double.TryParse(parts[0].Trim(), out beat) || !double.TryParse(parts[1].Trim(), out bpm))
                    throw new FieldFormatException("BPMs are not formatted correctly (not a valid number).");

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
                    throw new FieldFormatException("Stops are not formatted correctly (expected a beat=time pair).");

                double beat;
                double seconds;

                // Try parsing the beat and time parts.
                if (!double.TryParse(parts[0].Trim(), out beat) || !double.TryParse(parts[1].Trim(), out seconds))
                    throw new FieldFormatException("Stops are not formatted correctly (not a valid number).");

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

        /// <summary>
        /// Parses and returns a chart.
        /// </summary>
        /// <param name="data">The data from a #NOTES field.</param>
        public static Chart ParseChart(string data)
        {
            var chart = new Chart();
            var parts = data.Split(':');

            if (parts.Length != 6)
                throw new FieldFormatException("Note data is not formatted correctly (expected six arguments).");

            ParseChartHeaders(data, ref chart);
            chart.Measures = ParseNoteData(parts[5], chart.GetKeyCount());

            return chart;
        }

        /// <summary>
        /// Parses the headers/metadata for the chart.
        /// </summary>
        /// <param name="data">The data from a #NOTES field.</param>
        /// <param name="chart">The chart object to write to.</param>
        public static void ParseChartHeaders(string data, ref Chart chart)
        {
            var parts = data.Split(':');

            // Trim any excess whitespace.
            parts = parts.Select(p => p.Trim()).ToArray();

            chart.ChartType = parts[0];
            chart.Author = parts[1];
            chart.Difficulty = ParseChartDifficulty(parts[2]);

            try
            {
                chart.DifficultyRating = int.Parse(parts[3]);
            }
            catch (FormatException)
            {
                throw new FieldFormatException(
                    "Chart data is not formatted correctly (numerical difficulty rating is not valid)."
                );
            }

            chart.GrooveRadarValues = parts[4];

            var keyCount = chart.GetKeyCount();

            // We need to know the key count to parse the note data.
            if (keyCount == -1)
                throw new FieldFormatException($"Unrecognized chart type '{chart.ChartType}'.");
        }

        public static ChartDifficulty ParseChartDifficulty(string data)
        {
            data = data.ToLower();

            switch (data)
            {
                case "beginner":
                    return ChartDifficulty.Beginner;

                case "easy":
                    return ChartDifficulty.Easy;

                case "medium":
                    return ChartDifficulty.Medium;

                case "hard":
                    return ChartDifficulty.Hard;

                case "challenge":
                case "insane":
                    return ChartDifficulty.Insane;

                case "edit":
                    return ChartDifficulty.Edit;

                default:
                    Log.Warning($"Unrecognized chart type '{data}'. Defaulting to 'Edit'.");
                    return ChartDifficulty.Edit;
            }
        }

        /// <summary>
        /// Parses note data and returns a list of measures.
        /// </summary>
        /// <param name="data">The note data. This is only the note data, it does not include the headers.</param>
        public static List<Measure> ParseNoteData(string data, int keyCount)
        {
            var measureList = new List<Measure>();

            // Remove whitespace.
            data = Regex.Replace(data, @"\s", "");
            var measureData = data.Split(',');

            // Loop through each measure.
            for (var i = 0; i < measureData.Length; i++)
            {
                var curData = measureData[i];
                var subdivisions = curData.Length / (float)keyCount;

                if (subdivisions > Math.Floor(subdivisions))
                {
                    throw new FieldFormatException(
                        "Chart data is not formatted correctly (note count is not divisible by key count)."
                    );
                }

                var measure = new Measure(i, (int)subdivisions);

                // Loop through each beat row.
                for (var j = 0; j < subdivisions; j++)
                {
                    var beatRow = new BeatRow(keyCount);

                    // Loop through each key in the beat row.
                    for (var k = 0; k < keyCount; k++)
                    {
                        switch (curData[j])
                        {
                            case (char)NoteType.Empty:
                                break;

                            case (char)NoteType.Fake:
                                beatRow.Set(k, NoteType.Fake);
                                break;

                            case (char)NoteType.HoldHead:
                                beatRow.Set(k, NoteType.HoldHead);
                                break;

                            case (char)NoteType.RollHead:
                                beatRow.Set(k, NoteType.RollHead);
                                break;

                            case (char)NoteType.HoldRollTail:
                                beatRow.Set(k, NoteType.HoldRollTail);
                                break;

                            case (char)NoteType.Tap:
                                beatRow.Set(k, NoteType.Tap);
                                break;

                            case (char)NoteType.Mine:
                                beatRow.Set(k, NoteType.Mine);
                                break;

                            case (char)NoteType.Lift:
                                beatRow.Set(k, NoteType.Lift);
                                break;

                            case (char)NoteType.KeySound:
                                beatRow.Set(k, NoteType.KeySound);
                                break;

                            default:
                                Log.Warning($"Unknown note type '{curData[j]}'");
                                break;
                        }
                    }

                    measure.AddRow(j, beatRow);
                }
            }

            return measureList;
        }
    }
}
