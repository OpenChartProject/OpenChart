#!/bin/bash

set -e
cd ${0%/*}

: '
    ~~~~~~~~~~~~~~~
    |  Functions  |
    ~~~~~~~~~~~~~~~
'

function fnApplyVersion() {
    : '
    Updates the files in the project which reference the project version.
    This function expects each file to have a corresponding .template file
    which contains the $VERSION* vars to be substituted.
    '
    local files=(
        OpenChart/OpenChart.csproj
        OpenChart/installer/win-x64/setup.isl
    )

    export VERSION VERSION_MAJOR VERSION_MINOR VERSION_PATCH

    # Update the version across the project files.
    for f in "${files[@]}"; do
        if [[ ! -e "$f.template" ]]; then
            echo "WARNING: Template file is missing at: $f.template"
        else
            envsubst < $f.template > $f
            echo "Updated $f.template  -->  $f"
        fi
    done
}

function fnBuild() {
    : '
    Builds OpenChart in debug mode.
    * Arg 1: The build output path.
    '
    echo "-> Building OpenChart to $1/"

    dotnet build -o $1 $PROJECT_FILE
    fnCopyAssets $1
    fnCopyLibs $1
    fnCopyMisc $1
}

function fnBundle() {
    : '
    Bundles the published app into an installer.
    * Arg 1: The path containing the published app.
    * Arg 2: The path to the installer files.
    '
    if isWindows; then
        mkdir $BUNDLE_DIR
        cp -R $INSTALLER_DIR/$PLATFORM/* $BUNDLE_DIR
        cp -R $PUBLISH_DIR/$PLATFORM $BUNDLE_DIR/$PLATFORM

        local setup_path=`pwd`/$BUNDLE_DIR/setup.isl

        # Run Inno setup.
        iscc "$setup_path"
    else
        echo "Bundling is not supported on this OS."
    fi
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
    cp -r $ASSETS_DIR/* $1

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
    cp -r $LIB_DIR/$PLATFORM/* $1
}

function fnCopyMisc() {
    : '
    Copies all other files to the output directory.
    * Arg 1: The copy destination path.
    '
    if [[ -e "$MISC_DIR/$PLATFORM/" ]]; then
        echo "-> Copying runtime assets to $1/"
        cp -r -p $MISC_DIR/$PLATFORM/* $1
    fi
}

function fnPublish() {
    : '
    Builds OpenChart bundled as a single executable.
    '
    local out_dir=$PUBLISH_DIR/$PLATFORM

    echo "-> Publishing OpenChart to $out_dir/"

    export SkipGtkInstall=True

    rm -rf $out_dir
    dotnet publish -o $out_dir -r $PLATFORM -c Release OpenChart
    fnCopyAssets $out_dir
    fnCopyLibs $out_dir
    fnCopyMisc $out_dir
}

function fnRun() {
    : '
    Runs OpenChart from the output directory.
    '
    echo "-> Starting OpenChart"

    if isLinux or isMacOS; then
        $OUTPUT_DIR/OpenChart.sh
    elif isWindows; then
        $OUTPUT_DIR/OpenChart.exe
    fi
}

function fnTest() {
    : '
    Runs the test suite.
    '
    echo "-> Running test suite"

    local path=$TEST_DIR/bin/Debug/netcoreapp3.1

    fnCopyAssets $path
    fnCopyLibs $path
    fnCopyMisc $path

    OPENCHART_DIR=`pwd`/$path dotnet test
}

function fnVersion() {
    : '
    Prints the current version, or modifies the version if an arg is given.
    * Arg 1: (optional) Can be <major|minor|patch>
    '
    case "$1" in
        "")
        echo $VERSION
        exit
        ;;

        apply)
        fnApplyVersion
        exit
        ;;

        major)
        VERSION_MAJOR=$((VERSION_MAJOR + 1))
        VERSION_MINOR=0
        VERSION_PATCH=0
        ;;

        minor)
        VERSION_MINOR=$((VERSION_MINOR + 1))
        VERSION_PATCH=0
        ;;

        patch)
        VERSION_PATCH=$((VERSION_PATCH + 1))
        ;;

        *)
        echo "Usage: $0 version [command]"
        echo
        echo "COMMANDS"
        echo "If no command is given, prints the current version."
        echo
        echo "  apply   Applies the current version to any files which depend on it"
        echo "  major   Increments the major version"
        echo "  minor   Increments the minor version"
        echo "  patch   Increments the patch version"
        echo
        exit
        ;;
    esac

    # Update the version.
    export VERSION=$VERSION_MAJOR.$VERSION_MINOR.$VERSION_PATCH
    echo $VERSION > VERSION
    echo "Incremented version to $VERSION"

    fnApplyVersion
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
    echo "  build       Builds the project to $OUTPUT_DIR/"
    echo "  bundle      Bundles a published project to $BUNDLE_DIR/"
    echo "  clean       Cleans all build-related files"
    echo "  help        Prints this help message"
    echo "  publish     Builds the project for release"
    echo "  run         Runs OpenChart from $OUTPUT_DIR/"
    echo "  test        Runs the test suite"
    echo "  version     Prints the current version"
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
BUNDLE_DIR=bundle
OUTPUT_DIR=bin
PUBLISH_DIR=dist
ORIGINAL_ASSETS_DIR=__original__

PROJECT_DIR=OpenChart
PROJECT_FILE=$PROJECT_DIR/OpenChart.csproj

TEST_DIR=OpenChart.Tests

ASSETS_DIR=$PROJECT_DIR/assets
INSTALLER_DIR=$PROJECT_DIR/installer
LIB_DIR=$PROJECT_DIR/lib
MISC_DIR=$PROJECT_DIR/misc

VERSION=`cat VERSION`
VERSION_ARRAY=(`echo $VERSION | tr '.' ' '`)
VERSION_MAJOR=${VERSION_ARRAY[0]}
VERSION_MINOR=${VERSION_ARRAY[1]}
VERSION_PATCH=${VERSION_ARRAY[2]}

DETECTED_OS=$OS

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

case "$1" in
    "")
    fnBuild $OUTPUT_DIR
    fnRun
    ;;

    build)
    fnBuild $OUTPUT_DIR
    ;;

    bundle)
    fnBundle "$PUBLISH_DIR/$PLATFORM" "$INSTALLER_DIR/$PLATFORM"
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
