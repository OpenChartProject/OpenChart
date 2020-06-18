using OpenChart.Charting;
using OpenChart.Projects;
using System;

namespace OpenChart
{
    /// <summary>
    /// An event bus that fires off application-wide events.
    /// </summary>
    public class ApplicationEventBus
    {
        /// <summary>
        /// The application data for this event bus.
        /// </summary>
        public ApplicationData ApplicationData;

        /// <summary>
        /// An event fired when a chart is added to the project.
        /// </summary>
        public event EventHandler<ChartAddedArgs> ChartAdded;

        /// <summary>
        /// An event fired when the current active project for the app changes.
        /// </summary>
        public event EventHandler<ProjectChangedEventArgs> CurrentProjectChanged;

        /// <summary>
        /// An event fired when the project's name is changed.
        /// </summary>
        public event EventHandler CurrentProjectRenamed;

        public ApplicationEventBus(ApplicationData appData)
        {
            this.ApplicationData = appData;

            appData.ProjectChanged += onCurrentProjectChanged;
        }

        /// <summary>
        /// Handler for when a new chart is added to the current project.
        /// </summary>
        private void onChartAdded(object o, ChartAddedArgs e)
        {
            ChartAdded?.Invoke(this, e);
        }

        /// <summary>
        /// Handler for when the current project changes. This will add/remove all project-related
        /// event listeners from the event bus as needed.
        /// </summary>
        private void onCurrentProjectChanged(object o, ProjectChangedEventArgs e)
        {
            // Remove listeners on the old project.
            if (e.OldProject != null)
            {
                e.OldProject.ChartAdded -= onChartAdded;
                e.OldProject.Renamed -= onCurrentProjectRenamed;
            }

            // Add listeners to the new project.
            if (e.NewProject != null)
            {
                e.NewProject.ChartAdded += onChartAdded;
                e.NewProject.Renamed += onCurrentProjectRenamed;
            }

            CurrentProjectChanged?.Invoke(this, e);
        }

        private void onCurrentProjectRenamed(object o, EventArgs e)
        {
            CurrentProjectRenamed?.Invoke(this, e);
        }
    }
}
