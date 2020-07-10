@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Building and packing all the things! ---
msbuild /t:clean;restore;build;pack %* %THIS_DIR%\CI\CI.Windows.proj
