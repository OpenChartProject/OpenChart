using OpenChart.Charting;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Projects;
using OpenChart.Songs;
using System.Collections.Generic;

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
                    chart.BPMs.AddMultiple(c.BPMs);
                    project.Charts.Add(chart);
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
                chart.KeyCount = c.KeyCount;
                chart.BPMs = c.BPMs.ToArray();
                chartList.Add(chart);
            }

            pd.Charts = chartList.ToArray();

            return pd;
        }
    }
}