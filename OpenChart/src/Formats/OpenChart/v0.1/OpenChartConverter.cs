using OpenChart.Charting;
using OpenChart.Charting.Properties;
using NativeObjects = OpenChart.Charting.Objects;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using FormatObjects = OpenChart.Formats.OpenChart.Version0_1.Objects;
using OpenChart.Projects;
using OpenChart.Songs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// A converter for transforming Project objects into FileData objects and vice versa.
    /// </summary>
    public class OpenChartConverter : IProjectConverter<ProjectData>
    {
        /// <summary>
        /// This converter supports exporting multiple charts into a single file.
        /// </summary>
        public static bool SupportsMultipleExports => true;

        /// <summary>
        /// Converts an OpenChart file into a Project instance.
        /// </summary>
        /// <param name="data">The project data loaded from an OpenChart file.</param>
        public Project ToNative(ProjectData data)
        {
            try
            {
                data.Validate();
            }
            catch (Exception e)
            {
                throw new ConverterException(
                    "An error occurred while loading in some project data. The file may " +
                    "be missing some data, is corrupted, or is a version not currently supported.",
                    e
                );
            }

            var project = new Project();

            // Add the song data if it's present
            if (data.Song != null)
            {
                project.SongMetadata = new SongMetadata();
                project.SongMetadata.Artist = data.Song.Artist;
                project.SongMetadata.AudioFilePath = data.Song.Path;
                project.SongMetadata.Title = data.Song.Title;
            }

            // Convert each chart in the project
            if (data.Charts != null)
            {
                foreach (var c in data.Charts)
                {
                    var chart = new Chart(c.KeyCount);
                    project.Charts.Add(chart);

                    chart.BPMList.BPMs.AddMultiple(c.BPMs);
                    chart.Author = c.Author;
                    chart.ChartName = c.ChartName;

                    // Iterate through each beat row.
                    foreach (var row in c.Rows)
                    {
                        // Iterate through each key index for the beat row.
                        for (var keyIndex = 0; keyIndex < chart.KeyCount.Value; keyIndex++)
                        {
                            // No object here, skip.
                            if (row.Objects[keyIndex] == null)
                                continue;

                            // Map to the appropriate object type.
                            if (row.Objects[keyIndex] is FormatObjects.TapNote)
                            {
                                chart.Objects[keyIndex].Add(
                                    new NativeObjects.TapNote(keyIndex, row.Beat)
                                );
                            }
                            else if (row.Objects[keyIndex] is FormatObjects.HoldNote holdNote)
                            {
                                chart.Objects[keyIndex].Add(
                                    new NativeObjects.HoldNote(keyIndex, row.Beat, holdNote.Length)
                                );
                            }
                            else
                                throw new ConverterException("Unknown object type");
                        }
                    }
                }
            }

            return project;
        }

        /// <summary>
        /// Converts a Project instance into an OpenChart FileData instance.
        /// </summary>
        /// <param name="project">A native Project.</param>
        public ProjectData FromNative(Project project)
        {
            var pd = new ProjectData();
            pd.Metadata.Version = OpenChartFormatHandler.Version;

            // Add the song data if it's present
            if (project.SongMetadata != null)
            {
                pd.Song = new SongData();
                pd.Song.Artist = project.SongMetadata.Artist;
                pd.Song.Title = project.SongMetadata.Title;
                pd.Song.Path = project.SongMetadata.AudioFilePath;
            }

            var chartList = new List<ChartData>(project.Charts.Count);

            // Convert each chart in the project
            foreach (var c in project.Charts)
            {
                var chart = new ChartData();
                chartList.Add(chart);

                chart.KeyCount = c.KeyCount;
                chart.Author = c.Author;
                chart.ChartName = c.ChartName;
                chart.BPMs = c.BPMList.BPMs.ToArray();

                // Beat rows are stored as a dictionary. This is necessary because the
                // native Chart class stores objects by key index, whereas the file
                // format stores them by row. As we iterate through each key index we
                // need to be able to retrieve the beat row at that particular beat.
                var dict = new Dictionary<Beat, BeatRowData>();

                // Iterate through each key index.
                for (var keyIndex = 0; keyIndex < chart.KeyCount.Value; keyIndex++)
                {
                    // Get each object for this key index.
                    foreach (var keyObject in c.Objects[keyIndex])
                    {
                        BeatRowData row;
                        FormatObjects.IChartObject obj;

                        // Try getting the beat row, or create one if it doesn't exist yet.
                        if (!dict.ContainsKey(keyObject.Beat))
                        {
                            row = new BeatRowData(chart.KeyCount.Value);
                            row.Beat = keyObject.Beat;
                            dict[keyObject.Beat] = row;
                        }
                        else
                            row = dict[keyObject.Beat];

                        // Map to the appropriate object type.
                        if (keyObject is NativeObjects.TapNote)
                            obj = new FormatObjects.TapNote();
                        else if (keyObject is NativeObjects.HoldNote holdNote)
                        {
                            obj = new FormatObjects.HoldNote
                            {
                                Length = holdNote.Length
                            };
                        }
                        else
                            throw new ConverterException("Unknown object type");

                        row.Objects[keyIndex] = obj;
                    }
                }

                // Get the beat rows from the dictionary and sort them by beat.
                var rowList = new List<BeatRowData>(dict.Count);

                rowList.AddRange(dict.Values);
                rowList.Sort();

                chart.Rows = rowList.ToArray();
            }

            pd.Charts = chartList.ToArray();

            try
            {
                pd.Validate();
            }
            catch (Exception e)
            {
                throw new ConverterException(
                    "An unexpected error occurred while processing the project.",
                    e
                );
            }

            return pd;
        }
    }
}
