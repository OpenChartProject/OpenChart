using GLib;

namespace OpenChart.UI.Actions
{
    public class MenuModel
    {
        public Menu FileMenu;
        public Menu EditMenu;
        public Menu HelpMenu;

        public MenuModel()
        {
            FileMenu = new Menu();
            FileMenu.Append("New Project", "file.new_project");
            FileMenu.Append("Open Project", "file.open_project");
            FileMenu.Append("Save", "file.save");
            FileMenu.Append("Save As...", "file.save_as");
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
