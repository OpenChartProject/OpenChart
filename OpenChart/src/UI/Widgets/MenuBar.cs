using Gtk;

namespace OpenChart.UI.Widgets
{
    public class MenuBar : Gtk.MenuBar
    {
        public MenuBar()
        {
            Menu subMenu;
            MenuItem menu;

            {
                menu = new MenuItem("_File");
                Add(menu);

                subMenu = new Menu();
                menu.Submenu = subMenu;

                addMenuItem(subMenu, "New Project", null);
                addMenuItem(subMenu, "Open Project", null);
                subMenu.Add(new SeparatorMenuItem());
                addMenuItem(subMenu, "Save", null);
                addMenuItem(subMenu, "Save As...", null);
                subMenu.Add(new SeparatorMenuItem());
                addMenuItem(subMenu, "Exit", null);
            }

            {
                menu = new MenuItem("_Edit");
                Add(menu);

                subMenu = new Menu();
                menu.Submenu = subMenu;

                addMenuItem(subMenu, "Undo", null);
                addMenuItem(subMenu, "Redo", null);
                subMenu.Add(new SeparatorMenuItem());
                addMenuItem(subMenu, "Cut", null);
                addMenuItem(subMenu, "Copy", null);
                addMenuItem(subMenu, "Paste", null);
            }

            {
                menu = new MenuItem("_Help");
                Add(menu);

                subMenu = new Menu();
                menu.Submenu = subMenu;

                addMenuItem(subMenu, "About", null);
                addMenuItem(subMenu, "Visit Website", null);
            }
        }

        private void addMenuItem(Menu subMenu, string label, string actionName)
        {
            var item = new MenuItem(label);
            item.ActionName = actionName;
            subMenu.Add(item);
        }
    }
}
