@ECHO OFF

SETLOCAL
SET "PATH=%PATH%;lib;lib\gtk"

cd /D "%~dp0"

START OpenChart.exe
