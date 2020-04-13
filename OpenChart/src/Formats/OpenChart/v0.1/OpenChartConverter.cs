using OpenChart.Charting;
using OpenChart.Formats.OpenChart.Version0_1.Data;
using OpenChart.Projects;
using System.Collections.Generic;

namespace OpenChart.Formats.OpenChart.Version0_1
{
    /// <summary>
    /// A converter for transforming Project objects into FileData objects and vice versa.
    /// </summary>
    public class OpenChartConverter : IProjectConverter<ProjectData>
    {
        public bool SupportsMultipleExports => true;

        /// <summary>
        /// Converts an OpenChart file into a Project instance.
        /// </summary>
        /// <param name="data">The project data loaded from an OpenChart file.</param>
        public Project ToNative(ProjectData data)
        {
            var project = new Project();

            project.SongMetadata.Artist = data.Song.Artist;
            project.SongMetadata.AudioFilePath = data.Song.Path;
            project.SongMetadata.Title = data.Song.Title;

            foreach (var c in data.Charts)
            {
                var chart = new Chart(c.KeyCount);
                project.Charts.Add(chart);
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
            fd.Metadata.Version = "0.1";

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