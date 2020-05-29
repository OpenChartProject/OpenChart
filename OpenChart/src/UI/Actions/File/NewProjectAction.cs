using Gtk;
using OpenChart.Projects;
using Serilog;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// An action that triggers the application to create a new project.
    /// </summary>
    public class NewProjectAction : Actions.IAction
    {
        public static string Hotkey => "<Control>n";
        public static string Name => "file.new_project";

        GLib.SimpleAction _action;
        public GLib.IAction Action => (GLib.IAction)_action;

        /// <summary>
        /// Creates a new NewProjectAction instance.
        /// </summary>
        public NewProjectAction()
        {
            _action = new GLib.SimpleAction(Name, null);
            _action.Activated += OnActivated;
            _action.Enabled = true;
        }

        protected void OnActivated(object o, GLib.ActivatedArgs args)
        {
            var app = Application.GetInstance();

            if (app.AppData.CurrentProject != null)
            {
                // TODO: Check if the project has been modified and prompt the user to save.
                var dialog = new MessageDialog(
                    app.ActiveWindow,
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
                app.AppData.CurrentProject = new Project();
                Log.Information("Created a new project instance.");
            }
        }
    }
}
