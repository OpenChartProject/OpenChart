using Gtk;
using OpenChart.Projects;
using Serilog;

namespace OpenChart.UI.MenuActions
{
    /// <summary>
    /// An action that triggers the application to create a new project.
    /// </summary>
    public class NewProjectAction : MenuActions.IAction
    {
        IApplication app;

        public const string Hotkey = "<Control>n";
        public string GetHotkey() => Hotkey;

        public const string Name = "file.new_project";
        public string GetName() => Name;

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new NewProjectAction instance.
        /// </summary>
        public NewProjectAction(IApplication app)
        {
            this.app = app;

            _action = new GLib.SimpleAction(GetName(), null);
            _action.Activated += OnActivated;
            _action.Enabled = true;
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            if (app.GetData().CurrentProject != null)
            {
                // TODO: Check if the project has been modified and prompt the user to save.
                var dialog = new MessageDialog(
                    app.GetGtk().ActiveWindow,
                    DialogFlags.Modal,
                    MessageType.Warning,
                    ButtonsType.Ok,
                    "There is already an active project open."
                );

                dialog.Response += delegate { dialog.Dispose(); };
                dialog.ShowAll();
            }
            else
            {
                app.GetData().CurrentProject = new Project();
                Log.Information("Created a new project instance.");
            }
        }
    }
}
