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

                subMenu.Add(new MenuItem("New Project"));
                subMenu.Add(new MenuItem("Open Project"));
                subMenu.Add(new SeparatorMenuItem());
                subMenu.Add(new MenuItem("Save"));
                subMenu.Add(new MenuItem("Save As..."));
                subMenu.Add(new SeparatorMenuItem());
                subMenu.Add(new MenuItem("Exit"));
            }

            {
                menu = new MenuItem("_Edit");
                Add(menu);

                subMenu = new Menu();
                menu.Submenu = subMenu;

                subMenu.Add(new MenuItem("Undo"));
                subMenu.Add(new MenuItem("Redo"));
                subMenu.Add(new SeparatorMenuItem());
                subMenu.Add(new MenuItem("Cut"));
                subMenu.Add(new MenuItem("Copy"));
                subMenu.Add(new MenuItem("Paste"));
            }

            {
                menu = new MenuItem("_Help");
                Add(menu);

                subMenu = new Menu();
                menu.Submenu = subMenu;

                subMenu.Add(new MenuItem("About"));
                subMenu.Add(new MenuItem("Visit Website"));
            }
        }
    }
}
