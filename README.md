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

- **Using VSCode on Linux?** [Please read this.](docs/VSCode.md)

# Building OpenChart 🔨

OpenChart uses the [.NET Core v3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).

[GTK v3.x](https://www.gtk.org/) must be installed to build and run the project. This really should come already bundled with the project (like how `libbass` is). If you would like to add these dependencies it would be greatly appreciated!

### GTK for Ubuntu/Debian
```
sudo apt install libgtk-3-0
```

### GTK for macOS
```
brew install gtk+3
```

### GTK for Windows
Follow the installation directions [here](https://www.gtk.org/docs/installations/windows/).

Alternatively, if `pacman` is installed:

```
pacman -S mingw-w64-x86_64-gtk3
```

### Makefile

- `make`: builds and runs OpenChart.
- `make build`: compiles OpenChart and copies dependencies to the output folder.
- `make run`: starts OpenChart.
- `make test`: runs the test suite.
- `make clean`: cleans `bin/` folders.
- `make cleanall`: cleans both `bin/` and `obj/` folders. Run `dotnet restore` to redownload dependencies.
- `make publish`: publishes the project for Linux, macOS, and Windows into `dist/`.
- `make publish-linux`: publishes the project for Linux x64 into `dist/`.
- `make publish-osx`: publishes the project for macOS x64 into `dist/`.
- `make publish-win`: publishes the project for Windows x64 into `dist/`.

# License

The OpenChart project is dual-licensed.

All code and non-branding related assets are protected under the GNU GPLv3 license. See the [LICENSE](LICENSE) for more information.

Branding images are protected under the Creative Commons Attribution-NonCommercial-NoDerivatives 4.0 International License. This applies to any artifacts located in the [branding/](branding) folder. See the [README](branding/README.md) for more information.
