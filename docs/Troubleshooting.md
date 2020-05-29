# Troubleshooting

This doc lists some common issues and possible solutions/workarounds.

---

**Q:** I have an issue that isn't listed here.

**A:** Send a message in our [discord server](https://discord.gg/wSGmN52) or create a new [GitHub issue](https://github.com/OpenChartProject/OpenChart/issues).

---

**Q:** Why not just fix these issues?

**A:** There are a handful of reasons why:

- The issue can't be fixed in the code.
- The amount of time and effort to fix the issue is too much compared to its solution/workaround.
- What might be a fix for one person could create an issue for someone else.

---

## The app doesn't open.
- Try checking the logs to see what is causing the problem `<install_path>/logs/OpenChart.log`

## There is an error in the logs that says Gtk can't be found.
**Windows**:
- Try reinstalling OpenChart (this will automatically install Gtk if it's missing).
- Try installing Gtk manually ([download](https://github.com/tschoonj/GTK-for-Windows-Runtime-Environment-Installer/releases/download/2020-05-19/gtk3-runtime-3.24.18-2020-05-19-ts-win64.exe)).
Accept the defaults for the installer, and make sure the option to include Gtk in your PATH is checked.

**Linux/MacOS**:
- Try installing the gtk package for your system ([more info](https://github.com/OpenChartProject/OpenChart#linux)).
- Try verifying the libs exist: `find /usr -name "libgtk*" -type f,l`
- Try editing the `OpenChart.sh` script and append the Gtk lib path to the *_LIBRARY_PATH var
  - On Ubuntu: `export LD_LIBRARY_PATH=$LD_LIBRARY_PATH:.:/usr/lib/x86_64-linux-gnu`

## (Windows) There is an error popup that mentions something about a procedure entry-point.
Example:

![](img/missing-procedure-entry-point.png)

This error can happen when trying to initialize Gtk. It's caused by Gtk and another app on your system both having a DLL
with the same name, but they are different versions. The naming conflict results in Gtk accidentally loading the wrong DLL.

To fix:

- Take note of the DLL name in the error (for the example it would be `libpng16-16.dll`)
- Go to your Gtk runtime folder (usually `C:\Program Files\GTK3-Runtime Win64\`)
- Open the `bin\` folder.
- Search for a DLL with the same name as in the error.
- Copy the DLL from the Gtk folder to the OpenChart folder. Make sure the DLL is in the same folder as `OpenChart.exe`
