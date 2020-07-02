@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Packing all the things! ---
msbuild /t:pack %* %THIS_DIR%\CI\CI.Windows.proj
