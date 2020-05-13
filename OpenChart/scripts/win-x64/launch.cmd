@ECHO OFF

SETLOCAL
SET "PATH=%PATH%;lib;lib\gtk\bin"

cd /D "%~dp0"

CALL OpenChart.exe
