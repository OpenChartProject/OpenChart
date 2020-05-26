#!/bin/bash

set -e

: '
    ~~~~~~~~~~~~~~~
    |  Functions  |
    ~~~~~~~~~~~~~~~
'

function fnBuild() {
    : '
    Builds OpenChart in debug mode.
    * Arg 1: The build output path.
    '
    echo "-> Building OpenChart to $1/"

	dotnet build -o $1 $PROJECT_FILE
    fnCopyAssets $1
    fnCopyLibs $1
}

function fnClean() {
    : '
    Removes all directories that are generated from building/publishing.
    '
    echo "-> Removing build files"
	rm -rf $OUTPUT_DIR $PUBLISH_DIR OpenChart/bin/ OpenChart/obj/ OpenChart.Tests/bin/ OpenChart.Tests/obj/
}

function fnCopyAssets() {
    : '
    Copies assets to the output directory.
    * Arg 1: The copy destination path.
    '
    echo "-> Copying assets to $1/"

    if isLinux; then
		cp -r $ASSETS_DIR/* $1
    elif isMacOS; then
		cp -r $ASSETS_DIR/* $1
    elif isWindows; then
		cp -r $ASSETS_DIR/* $1
    fi

    local find_path=find

    if [[ $TERM == "cygwin" ]]; then
        find_path=/bin/$find_path
    fi

    $find_path $1 -wholename "$1/**/$ORIGINAL_ASSETS_DIR" -type d \
		| xargs rm -r
}

function fnCopyLibs() {
    : '
    Copies libs to the output directory.
    * Arg 1: The copy destination path.
    '
    echo "-> Copying libs to $1/"

    if isLinux; then
		cp -r $LIB_DIR/$PLATFORM/* $1
    elif isMacOS; then
		cp -r $LIB_DIR/$PLATFORM/* $1
    elif isWindows; then
		cp -r $LIB_DIR/$PLATFORM/* $1
    fi
}

function fnPublish() {
    : '
    Builds OpenChart bundled as a single executable.
    '
    local out_dir=$PUBLISH_DIR/$PLATFORM

    echo "-> Publishing OpenChart to $out_dir/"

    rm -rf $out_dir
    dotnet publish -o $out_dir -r $PLATFORM -c Release OpenChart
    fnCopyAssets $out_dir
    fnCopyLibs $out_dir
}

function fnRun() {
    : '
    Runs OpenChart from the output directory.
    '
    echo "-> Starting OpenChart"

    if isLinux || isMacOS; then
        $OUTPUT_DIR/launch.sh
    elif isWindows; then
        if [[ $TERM == "cygwin" ]]; then
            $OUTPUT_DIR/launch.sh
        else
            $OUTPUT_DIR/launch.cmd
        fi
    fi
}

function fnTest() {
    : '
    Runs the test suite.
    '
    echo "-> Running test suite"
    dotnet test
}

function fnVersion() {
    : '
    Prints the current version, or modifies the version if an arg is given.
    * Arg 1: (optional) Can be <major|minor|patch>
    '
    case "$1" in
        "")
        echo $VERSION
        ;;

        major)
        echo "$((VERSION_MAJOR + 1)).0.0" > VERSION
        cat VERSION
        ;;

        minor)
        echo "$VERSION_MAJOR.$((VERSION_MINOR + 1)).0" > VERSION
        cat VERSION
        ;;

        patch)
        echo "$VERSION_MAJOR.$VERSION_MINOR.$((VERSION_PATCH + 1))" > VERSION
        cat VERSION
        ;;

        *)
        echo "Usage: $0 version [command]"
        echo
        echo "COMMANDS"
        echo "If no command is given, prints the current version."
        echo
        echo "  major   Increments the major version"
        echo "  minor   Increments the minor version"
        echo "  patch   Increments the patch version"
        echo
        ;;
    esac
}

function fnUsage() {
    : '
    Prints the usage message and exits with code 1.
    '
    echo "Usage: $0 [command]"
    echo "A task runner script for automating building, publishing, testing, etc."
    echo
    echo "COMMANDS"
    echo "If no command is given, runs 'build' then 'run'."
    echo
    echo "  build     Builds the project to $OUTPUT_DIR/"
    echo "  clean     Cleans all build-related files"
    echo "  help      Prints this help message"
    echo "  publish   Builds the project for release"
    echo "  run       Runs OpenChart from $OUTPUT_DIR/"
    echo "  test      Runs the test suite"
    echo "  version   Prints the current version"
    echo
    echo "EXIT CODE"
    echo "Returns 0 if everything is OK."

    exit 1
}

: '
    ~~~~~~~~~~~~~~~~
    |  OS Helpers  |
    ~~~~~~~~~~~~~~~~
'

function isLinux() {
    : '
    Returns true if this is running on Linux.
    '
    [[ $DETECTED_OS == "Linux" ]]
}

function isMacOS() {
    : '
    Returns true if this is running on MacOS.
    '
    [[ $DETECTED_OS == "Darwin" ]]
}

function isWindows() {
    : '
    Returns true if this is running on Windows.
    '
    [[ $DETECTED_OS == "Windows_NT" ]]
}

function isSupportedOS() {
    isLinux || isMacOS || isWindows
}

: '
    ~~~~~~~~~~~~~~~
    |  Variables  |
    ~~~~~~~~~~~~~~~
'

ASSETS_DIR=$PROJECT_DIR/assets
DETECTED_OS=$OS
LIB_DIR=$PROJECT_DIR/lib
ORIGINAL_ASSETS_DIR=__original__
OUTPUT_DIR=bin
PROJECT_DIR=OpenChart
PROJECT_FILE=$PROJECT_DIR/OpenChart.csproj
PUBLISH_DIR=dist
SCRIPTS_DIR=$PROJECT_DIR/scripts
VERSION=`head VERSION -n 1`
VERSION_ARRAY=(`echo $VERSION | tr '.' ' '`)
VERSION_MAJOR=${VERSION_ARRAY[0]}
VERSION_MINOR=${VERSION_ARRAY[1]}
VERSION_PATCH=${VERSION_ARRAY[2]}

if [[ -z $DETECTED_OS ]]; then
    DETECTED_OS=$(uname)
fi

if isLinux; then
    PLATFORM=linux-x64
elif isMacOS; then
    PLATFORM=osx-x64
elif isWindows; then
    PLATFORM=win-x64
fi

: '
    ~~~~~~~~~~~~~~~~~~~
    |  Runner Script  |
    ~~~~~~~~~~~~~~~~~~~
'

if ! isSupportedOS; then
    echo "ERROR: This OS is not supported ($DETECTED_OS)."
    exit 1
fi

cd ${0%/*}

case "$1" in
    "")
    fnBuild $OUTPUT_DIR
    fnRun
    ;;

    build)
    fnBuild $OUTPUT_DIR
    ;;

    clean)
    fnClean
    ;;

    publish)
    fnPublish
    ;;

    run)
    fnRun
    ;;

    test)
    fnTest
    ;;

    version)
    fnVersion $2
    ;;

    help | "-h" | "--help")
    fnUsage
    ;;

    *)
    echo "ERROR: Unrecognized command: $1"
    echo
    fnUsage
    ;;
esac
