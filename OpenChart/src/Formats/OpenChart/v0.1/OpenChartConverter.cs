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

            if (data.Song != null)
            {
                project.SongMetadata = new SongMetadata();
                project.SongMetadata.Artist = data.Song.Artist;
                project.SongMetadata.AudioFilePath = data.Song.Path;
                project.SongMetadata.Title = data.Song.Title;
            }

            if (data.Charts != null)
            {
                foreach (var c in data.Charts)
                {
                    var chart = new Chart(c.KeyCount);
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
            var fd = new ProjectData();
            fd.Metadata.Version = OpenChartFormatHandler.Version;

            if (project.SongMetadata != null)
            {
                fd.Song = new SongData();
                fd.Song.Artist = project.SongMetadata.Artist;
                fd.Song.Title = project.SongMetadata.Title;
                fd.Song.Path = project.SongMetadata.AudioFilePath;
            }

            var chartList = new List<ChartData>(project.Charts.Count);

            foreach (var c in project.Charts)
            {
                var chart = new ChartData();
                chart.KeyCount = c.KeyCount;
            }

            fd.Charts = chartList.ToArray();

            return fd;
        }
    }
}