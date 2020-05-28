![GitHub Workflow Status](https://img.shields.io/github/workflow/status/OpenChartProject/OpenChart/build-and-test?style=for-the-badge)![GitHub release (latest by date)](https://img.shields.io/github/v/release/OpenChartProject/OpenChart?style=for-the-badge)![GitHub All Releases](https://img.shields.io/github/downloads/OpenChartProject/OpenChart/total?style=for-the-badge)

[![](https://imgur.com/bhQKKSZ.png)](https://discord.gg/wSGmN52)

--------------

[![](branding/banner_small.png)](https://github.com/OpenChartProject/OpenChart)

OpenChart is a free, open source, cross platform tool for creating rhythm game charts and maps. OpenChart is currently in active development.

We plan to add support for the following games:

- [Etterna](https://etternaonline.com/) / [Stepmania](https://www.stepmania.com/)
- [Quaver](https://quavergame.com/)
- [Osu! (mania)](https://osu.ppy.sh/)

# FAQ
- **Looking for the latest build?** Check out the automated [publish workflow](https://github.com/OpenChartProject/OpenChart/actions?query=workflow%3Apublish). NOTE: You must be logged in to GitHub to download automated builds.

- **Using VSCode on Linux?** [Please read this.](docs/VSCodeLinux.md)

# Building OpenChart 🔨

OpenChart uses [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) and [Gtk 3.24](https://www.gtk.org/).

The .NET Core SDK is only necessary to build the project. When the app is published the .NET runtimes are bundled into the executable to make it more portable.

### Build Script

There is an included `tasks.sh` script for automating a lot of the development tasks, such as building and running the test suite.

Run `./tasks.sh --help` for usage info.

## Windows
In order to use the provided `tasks.sh` script you need to use a bash terminal such as [cygwin](https://www.cygwin.com/), [MinGW](http://mingw.org/), or [WSL](https://docs.microsoft.com/en-us/windows/wsl/about).

The official Windows builds come bundled with Gtk already. They are not included for development. If you want to build on Windows, do the following:

- [Download Gtk 3.24](https://github.com/GtkSharp/Dependencies/raw/master/gtk-3.24.zip)
- Extract the zip somewhere (e.g. `C:\Gtk-3.24`)
- Add the extracted folder to your PATH

## Linux
Linux users will need to install Gtk before the app can be run.

**Ubuntu/Debian**

```bash
sudo apt install libgtk-3-0
```

## MacOS
Mac users will need to install Gtk before the app can be run.

```bash
brew install gtk+3
```

# License

The OpenChart project is dual-licensed.

All code and non-branding related assets are protected under the GNU GPLv3 license. See the [LICENSE](LICENSE) for more information.

Branding images are protected under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. This applies to any artifacts located in the [branding/](branding) folder. See the [README](branding/README.md) for more information.
