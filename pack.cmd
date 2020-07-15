@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Building and packing all the things! ---

msbuild /t:clean %* %THIS_DIR%\CI\CI.Windows.proj

msbuild /t:restore %* %THIS_DIR%\CI\CI.Windows.proj

msbuild /t:build %* %THIS_DIR%\CI\CI.Windows.proj

msbuild /t:pack %* %THIS_DIR%\CI\CI.Windows.proj

msbuild /t:restore %* %THIS_DIR%\templates\MobileBlazorBindings-template-pack.csproj

msbuild /t:pack %* %THIS_DIR%\templates\MobileBlazorBindings-template-pack.csproj
