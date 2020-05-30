using OpenChart.Formats;
using OpenChart.Formats.OpenChart.Version0_1;
using OpenChart.NoteSkins;
using OpenChart.Projects;
using Serilog;
using System;

namespace OpenChart
{
    /// <summary>
    /// Event args used for the ProjectChanged event.
    /// </summary>
    public class ProjectChangedEventArgs : EventArgs
    {
        public readonly Project OldProject;
        public readonly Project NewProject;

        public ProjectChangedEventArgs(Project oldProject, Project newProject)
        {
            OldProject = oldProject;
            NewProject = newProject;
        }
    }

    /// <summary>
    /// The state of the application.
    /// </summary>
    public class ApplicationData
    {
        /// <summary>
        /// The absolute path to the folder where the OpenChart executable is.
        /// </summary>
        public string AppFolder { get; private set; }

        Project _currentProject;
        /// <summary>
        /// The current active project. This is null when no project is open.
        ///
        /// NOTE: If you want the save prompt to be triggered you should call
        /// <see cref="CloseCurrentProject" /> instead of directly setting this to null.
        /// </summary>
        public Project CurrentProject
        {
            get => _currentProject;
            set
            {
                if (CurrentProject == value)
                    return;

                var old = _currentProject;
                _currentProject = value;
                ProjectChanged?.Invoke(this, new ProjectChangedEventArgs(old, value));
            }
        }

        /// <summary>
        /// An event fired when the current project changes.
        /// </summary>
        public event EventHandler<ProjectChangedEventArgs> ProjectChanged;

        /// <summary>
        /// The manager for different file formats.
        /// </summary>
        public FormatManager Formats { get; private set; }

        /// <summary>
        /// The location of the noteskins folder.
        /// </summary>
        public string NoteSkinFolder => "noteskins";

        /// <summary>
        /// The noteskins that are loaded into the app.
        /// </summary>
        public NoteSkinManager NoteSkins { get; private set; }

        /// <summary>
        /// Creates a new ApplicationData instance.
        /// </summary>
        /// <param name="appFolder">The path the OpenChart executable is in.</param>
        public ApplicationData(string appFolder)
        {
            AppFolder = appFolder;
            Formats = new FormatManager();
            NoteSkins = new NoteSkinManager();
        }

        public void CloseCurrentProject()
        {
            if (CurrentProject == null)
                return;

            Log.Information($"Closing the '{CurrentProject.Name}' project.");

            CurrentProject = null;

            // TODO: Handle save logic.
        }

        /// <summary>
        /// Initializes the application data.
        /// </summary>
        public void Init()
        {
            Log.Debug("Setting up file formats.");
            Formats.AddFormat(new OpenChartFormatHandler());

            Log.Information("Finding noteskins...");
            NoteSkins.LoadAll(NoteSkinFolder);
        }
    }
}
