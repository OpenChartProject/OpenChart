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
            FileMenu.Append("New Project", null);
            FileMenu.Append("Open Project", null);
            FileMenu.Append("Save", null);
            FileMenu.Append("Save As...", null);
            FileMenu.Append("Exit", null);

            EditMenu = new Menu();
            EditMenu.Append("Undo", null);
            EditMenu.Append("Redo", null);
            EditMenu.Append("Cut", null);
            EditMenu.Append("Copy", null);
            EditMenu.Append("Paste", null);

            HelpMenu = new Menu();
            HelpMenu.Append("About", null);
            HelpMenu.Append("Website", null);
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
