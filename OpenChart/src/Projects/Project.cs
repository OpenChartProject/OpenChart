using OpenChart.Charting;
using OpenChart.Songs;
using System;
using System.Collections.Generic;

namespace OpenChart.Projects
{
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

        string _name;
        /// <summary>
        /// The name of the project.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Project name cannot be blank.");

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
    }
}
