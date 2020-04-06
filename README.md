# OpenChart

OpenChart is a free, open source, cross platform tool for creating rhythm game charts and maps.

**OpenChart is currently in active development.**

We plan to add support for the following games:

- [Etterna](https://etternaonline.com/) / [Stepmania](https://www.stepmania.com/)
- [Quaver](https://quavergame.com/)
- [Osu! (mania)](https://osu.ppy.sh/)

# Building OpenChart

OpenChart uses the [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1).

Nuget takes care of most of the dependencies. For the ones it doesn't (such as `libbass`), they can be found in `OpenChart/lib/`.

Makefile commands:

- `make`: builds and runs OpenChart.
- `make build`: compiles OpenChart and copies dependencies to the output folder.
- `make run`: starts OpenChart.
- `make test`: runs the test suite.
- `make clean`: cleans `bin/` folders.
- `make cleanall`: cleans both `bin/` and `obj/` folders. Run `dotnet restore` to redownload dependencies.

## Visual Studio Code

⚠️ Use an external terminal (i.e. not the one in VSCode) to start the app.

On linux (and possibly other platforms) there is an issue with the app crashing at startup if you try and run it from the terminal inside VSCode. VSCode injects some environment variables which causes both GTK and libbass to fail.