@ECHO OFF

SETLOCAL
SET "PATH=%PATH%;lib;lib\gtk\bin"

cd /D "%~dp0"

START OpenChart.exe
