using OpenChart.UI.Actions;
using System;

namespace OpenChart
{
    /// <summary>
    /// An event bus that fires off application-wide events.
    /// </summary>
    public class ApplicationEventBus
    {
        Application app;

        /// <summary>
        /// An event fired when the current active project for the app changes.
        /// </summary>
        public event EventHandler<ProjectChangedEventArgs> CurrentProjectChanged;

        /// <summary>
        /// An event fired when the project's name is changed.
        /// </summary>
        public event EventHandler CurrentProjectRenamed;

        public ApplicationEventBus(Application app)
        {
            this.app = app;

            app.AppData.ProjectChanged += onCurrentProjectChanged;
        }

        /// <summary>
        /// Handler for when the current project changes. This will add/remove all project-related
        /// event listeners from the event bus as needed.
        /// </summary>
        private void onCurrentProjectChanged(object o, ProjectChangedEventArgs e)
        {
            // Remove listeners on the old project.
            if (e.OldProject != null)
                e.OldProject.Renamed -= onCurrentProjectRenamed;

            // Add listeners to the new project.
            if (e.NewProject != null)
                e.NewProject.Renamed += onCurrentProjectRenamed;

            // Set the close project action to be enabled only if there is an active project open.
            app.ActionDict[CloseProjectAction.Name].SetEnabled(e.NewProject != null);

            CurrentProjectChanged?.Invoke(this, e);
        }

        private void onCurrentProjectRenamed(object o, EventArgs e)
        {
            CurrentProjectRenamed?.Invoke(o, e);
        }
    }
}
