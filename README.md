![GitHub Workflow Status](https://img.shields.io/github/workflow/status/OpenChartProject/OpenChart/build-and-test?style=for-the-badge)![GitHub All Releases](https://img.shields.io/github/downloads/OpenChartProject/OpenChart/total?style=for-the-badge)![GitHub release (latest by date)](https://img.shields.io/github/v/release/OpenChartProject/OpenChart?style=for-the-badge)

[![](https://imgur.com/bhQKKSZ.png)](https://discord.gg/wSGmN52)

--------------

[![](branding/banner_small.png)](https://github.com/OpenChartProject/OpenChart)

OpenChart is a free, open source, cross platform tool for creating rhythm game charts and maps. OpenChart is currently in active development.

We plan to add support for the following games:

- [Etterna](https://etternaonline.com/) / [Stepmania](https://www.stepmania.com/)
- [Quaver](https://quavergame.com/)
- [Osu! (mania)](https://osu.ppy.sh/)

# FAQ
- **Looking for the latest build?** Check out the automated [publish workflow](https://github.com/OpenChartProject/OpenChart/actions?query=workflow%3Apublish).

- **Using VSCode on Linux?** [Please read this.](docs/VSCodeLinux.md)

# Building OpenChart 🔨

OpenChart uses the [.NET Core v3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).

[GTK v3.24.x](https://www.gtk.org/) must be installed to start OpenChart. Windows users don't need to do anything here, as GTK binaries for Windows come bundled in the repo. Non-Windows users can install GTK via the terminal:

### GTK for Ubuntu/Debian
```
sudo apt install libgtk-3-0
```

### GTK for macOS
```
brew install gtk+3
```

### Tasks

There is an included `tasks.sh` script for automating a lot of the development tasks, such as building and running the test suite.

Run `./tasks.sh --help` for usage info.

# License

The OpenChart project is dual-licensed.

All code and non-branding related assets are protected under the GNU GPLv3 license. See the [LICENSE](LICENSE) for more information.

Branding images are protected under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. This applies to any artifacts located in the [branding/](branding) folder. See the [README](branding/README.md) for more information.
