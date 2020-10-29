using OpenChart.Projects;
using Serilog;
using System;
using System.IO;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the application to save the current project.
    /// </summary>
    public class SaveAsAction : Actions.IAction
    {
        IApplication app;

        public const string Hotkey = "<Control><Shift>S";
        public string GetHotkey() => Hotkey;

        public const string Name = "file.save_as";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new SaveAsAction instance.
        /// </summary>
        public SaveAsAction(IApplication app)
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

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            Log.Debug($"{this.GetType().Name} triggered.");

            var projectName = app.GetData().CurrentProject.Name;

            var dialog = new Gtk.FileChooserDialog(
                $"Save {projectName}",
                app.GetMainWindow(),
                Gtk.FileChooserAction.Save,
                "_Cancel",
                Gtk.ResponseType.Cancel,
                "_Save",
                Gtk.ResponseType.Accept,
                null
            );

            dialog.DoOverwriteConfirmation = true;
            dialog.CurrentName = projectName + ".oc";

            var resp = dialog.Run();

            if (resp == (int)Gtk.ResponseType.Accept)
                writeToFile(dialog.Filename, app.GetData().CurrentProject);

            dialog.Dispose();
        }

        private bool writeToFile(string filePath, Project project)
        {
            var fmt = app.GetData().Formats.GetFormatHandler(".oc");

            if (fmt == null)
            {
                Log.Error("Unable to save OC project, formatter not found.");
                return false;
            }

            try
            {
                using (var file = File.OpenWrite(filePath))
                {
                    using (var writer = new StreamWriter(file))
                    {
                        fmt.Write(writer, app.GetData().CurrentProject);
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Unable to save OC project.", e);
                return false;
            }

            Log.Information($"Saved project to: {filePath}");
            project.Path = filePath;

            return true;
        }
    }
}
