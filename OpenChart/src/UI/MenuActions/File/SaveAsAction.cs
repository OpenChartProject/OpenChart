using OpenChart.Projects;
using Serilog;
using System;
using System.IO;

namespace OpenChart.UI.MenuActions
{
    /// <summary>
    /// An action that triggers the application to save the current project.
    /// </summary>
    public class SaveAsAction : IMenuAction
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

            // Write the file if the user chose accept.
            if (resp == (int)Gtk.ResponseType.Accept)
            {
                SaveAction.WriteFile(
                    app.GetData().Formats.GetFormatHandler(".oc"),
                    dialog.Filename,
                    app.GetData().CurrentProject
                );
            }

            dialog.Dispose();
        }
    }
}
