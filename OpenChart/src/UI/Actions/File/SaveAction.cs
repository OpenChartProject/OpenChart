using OpenChart.Formats;
using OpenChart.Projects;
using Serilog;
using System;
using System.IO;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the application to save the current project.
    /// </summary>
    public class SaveAction : Actions.IAction
    {
        IApplication app;

        public const string Hotkey = "<Control>S";
        public string GetHotkey() => Hotkey;

        public const string Name = "file.save";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new SaveAction instance.
        /// </summary>
        public SaveAction(IApplication app)
        {
            this.app = app;

            _action = new GLib.SimpleAction(Name, null);
            _action.Activated += OnActivated;
            _action.Enabled = false;

            // Enable the action only when a project is open.
            app.GetEvents().CurrentProjectChanged += (o, e) =>
            {
                _action.Enabled = (e.NewProject != null);
            };
        }

        /// <summary>
        /// Writes the project to a file using the provided formatter. Returns true if
        /// the file was written successfully.
        /// </summary>
        public static bool WriteFile(IFormatHandler format, string filePath, Project project)
        {
            try
            {
                using (var file = File.OpenWrite(filePath))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        format.Write(writer, project);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to save project.", e);
                return false;
            }

            Log.Information($"Saved project to: {filePath}");
            project.Path = filePath;

            return true;
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            Log.Debug($"{this.GetType().Name} triggered.");

            var project = app.GetData().CurrentProject;

            // Show the Save As... dialog if the project hasn't been saved yet, otherwise save it
            // to its known path.
            if (project.Path == null)
            {
                Log.Debug("Project path has not been set.");
                app.GetGtk().ActivateAction(SaveAsAction.Name, null);
            }
            else
            {
                Log.Debug("Saving existing project.");
                WriteFile(app.GetData().Formats.GetFormatHandler(".oc"), project.Path, project);
            }
        }
    }
}
