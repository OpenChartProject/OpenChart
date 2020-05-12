#!/bin/bash

set -e

: '
    ~~~~~~~~~~~~~~~
    |  Functions  |
    ~~~~~~~~~~~~~~~
'

function fnBuild() {
    : '
    Builds OpenChart.
    * Arg 1: The build output path.
    '
    echo "-> Building OpenChart to $1/"
	dotnet build -o $1 $PROJECT_FILE
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

    find $1 -wholename "$1/**/$ORIGINAL_ASSETS_DIR" -type d \
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
    local lib_dir=$out_dir/lib

    echo "-> Publishing OpenChart to $out_dir/"

    rm -rf $out_dir
    dotnet publish -o $out_dir -r $PLATFORM -c Release OpenChart
    fnCopyAssets $out_dir
    mkdir $lib_dir
    fnCopyLibs $lib_dir
}

function fnRun() {
    : '
    Runs OpenChart from the output directory.
    '
    echo "-> Starting OpenChart"

    if isLinux; then
        $OUTPUT_DIR/OpenChart
    elif isMacOS; then
        dotnet $OUTPUT_DIR/OpenChart.dll
    elif isWindows; then
        $OUTPUT_DIR/OpenChart.exe
    fi
}

function fnTest() {
    : '
    Runs the test suite.
    '
    echo "-> Running test suite"
    dotnet test
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

PROJECT_DIR=OpenChart
PROJECT_FILE=$PROJECT_DIR/OpenChart.csproj
ASSETS_DIR=$PROJECT_DIR/assets
LIB_DIR=$PROJECT_DIR/lib
OUTPUT_DIR=bin
PUBLISH_DIR=dist
ORIGINAL_ASSETS_DIR=__original__
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

cd ${0%/*}

if [[ $# == 0 ]]; then
    fnBuild $OUTPUT_DIR
    fnCopyAssets $OUTPUT_DIR
    fnRun
else
    if [[ $1 == "build" ]]; then
        fnBuild $OUTPUT_DIR
    elif [[ $1 == "clean" ]]; then
        fnClean
    elif [[ $1 == "publish" ]]; then
        fnPublish
    elif [[ $1 == "run" ]]; then
        fnRun
    elif [[ $1 == "test" ]]; then
        fnTest
    elif [[ $1 == "help" ]] || [[ $1 == "-h" ]] || [[ $1 == "--help" ]]; then
        fnUsage
    else
        echo "ERROR: Unrecognized command."
        echo
        fnUsage
    fi
fi
