using OpenChart.Charting;
using OpenChart.Songs;
using Serilog;
using System;
using System.Collections.Generic;

namespace OpenChart.Projects
{
    /// <summary>
    /// Event args for when a chart is added to a project.
    /// </summary>
    public class ChartAddedArgs : EventArgs
    {
        public readonly Chart Chart;
        public readonly Project Project;

        public ChartAddedArgs(Chart chart, Project project)
        {
            Chart = chart;
            Project = project;
        }
    }

    /// <summary>
    /// A project represents a collection of one or more charts along with the song metadata
    /// (if there is a song).
    /// </summary>
    public class Project
    {
        /// <summary>
        /// A list of charts for this project.
        /// </summary>
        public List<Chart> Charts { get; private set; }

        /// <summary>
        /// An event fired when a chart is added to the project.
        /// </summary>
        public event EventHandler<ChartAddedArgs> ChartAdded;

        string _name;
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (value == Name)
                    return;

                _name = value;
                Renamed?.Invoke(this, null);
            }
        }

        /// <summary>
        /// An event fired when the project's name changes.
        /// </summary>
        public event EventHandler Renamed;

        /// <summary>
        /// The metadata for the song. Can be null.
        /// </summary>
        public SongMetadata SongMetadata { get; set; }

        /// <summary>
        /// Creates a new Project instance.
        /// </summary>
        public Project() : this("New Project") { }

        /// <summary>
        /// Creates a new Project instance.
        /// </summary>
        /// <param name="name">The name of the project.</param>
        public Project(string name)
        {
            Charts = new List<Chart>();
            Name = name;
            SongMetadata = null;
        }

        /// <summary>
        /// Adds a chart to the project and fires the <see cref="ChartAdded" /> event.
        /// </summary>
        /// <param name="chart">The chart to add.</param>
        public void AddChart(Chart chart)
        {
            if (Charts.Contains(chart))
                throw new ArgumentException("Chart already belongs to project.");

            Charts.Add(chart);
            Log.Information($"Add a {chart.KeyCount.Value} key chart to project '{Name}'.");
            ChartAdded?.Invoke(this, new ChartAddedArgs(chart, this));
        }
    }
}
