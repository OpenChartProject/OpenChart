using GLib;

namespace OpenChart.UI.MenuActions
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
            FileMenu = buildFileMenu();
            EditMenu = buildEditMenu();
            HelpMenu = buildHelpMenu();
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

        private Menu buildFileMenu()
        {
            Menu section;
            var menu = new Menu();

            section = new Menu();
            menu.AppendSection(null, section);

            section.Append("New Project", NewProjectAction.Name);
            section.Append("New Chart", NewChartAction.Name);
            section.Append("Open Project", "file.open_project");
            section.Append("Close", CloseProjectAction.Name);

            section = new Menu();
            menu.AppendSection(null, section);

            section.Append("Save", SaveAction.Name);
            section.Append("Save As...", SaveAsAction.Name);

            section = new Menu();
            menu.AppendSection(null, section);

            section.Append("Quit", QuitAction.Name);

            return menu;
        }

        private Menu buildEditMenu()
        {
            Menu section;
            var menu = new Menu();

            section = new Menu();
            menu.AppendSection(null, section);

            menu.Append("Undo", "edit.undo");
            menu.Append("Redo", "edit.redo");

            section = new Menu();
            menu.AppendSection(null, section);

            menu.Append("Cut", "edit.cut");
            menu.Append("Copy", "edit.copy");
            menu.Append("Paste", "edit.paste");

            return menu;
        }

        private Menu buildHelpMenu()
        {
            Menu section;
            var menu = new Menu();

            section = new Menu();
            menu.AppendSection(null, section);

            menu.Append("About", "help.about");
            menu.Append("Website", "help.website");

            return menu;
        }
    }
}
