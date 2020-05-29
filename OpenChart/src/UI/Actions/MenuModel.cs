using GLib;

namespace OpenChart.UI.Actions
{
    /// <summary>
    /// The model/layout of the menu shown in the <see cref="OpenChart.UI.Widgets.MenuBar" /> widget.
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// The "File" menu. All file actions are prefixed with "file."
        /// </summary>
        public Menu FileMenu;

        /// <summary>
        /// The "Edit" menu. All edit actions are prefixed with "edit."
        /// </summary>
        public Menu EditMenu;

        /// <summary>
        /// The "Help" menu. All help actions are prefixed with "help."
        /// </summary>
        public Menu HelpMenu;

        /// <summary>
        /// Creates a new MenuModel instance.
        /// </summary>
        public MenuModel()
        {
            FileMenu = new Menu();
            FileMenu.Append("New", NewProjectAction.Name);
            FileMenu.Append("Open", "file.open_project");
            FileMenu.Append("Close", CloseProjectAction.Name);
            FileMenu.Append("Save", SaveAction.Name);
            FileMenu.Append("Save As...", SaveAsAction.Name);
            FileMenu.Append("Quit", QuitAction.Name);

            EditMenu = new Menu();
            EditMenu.Append("Undo", "edit.undo");
            EditMenu.Append("Redo", "edit.redo");
            EditMenu.Append("Cut", "edit.cut");
            EditMenu.Append("Copy", "edit.copy");
            EditMenu.Append("Paste", "edit.paste");

            HelpMenu = new Menu();
            HelpMenu.Append("About", "help.about");
            HelpMenu.Append("Website", "help.website");
        }

        /// <summary>
        /// Returns the full menu model.
        /// </summary>
        public GLib.MenuModel GetModel()
        {
            var model = new Menu();

            model.AppendSubmenu("_File", FileMenu);
            model.AppendSubmenu("_Edit", EditMenu);
            model.AppendSubmenu("_Help", HelpMenu);

            return model;
        }
    }
}
