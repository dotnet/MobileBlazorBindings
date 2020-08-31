@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Building and packing all the things! ---

msbuild /t:clean %* %THIS_DIR%\CI\CI.Windows.proj

if errorlevel 1 exit /b %ERRORLEVEL%

msbuild /t:restore %* %THIS_DIR%\CI\CI.Windows.proj

if errorlevel 1 exit /b %ERRORLEVEL%

msbuild /t:build %* %THIS_DIR%\CI\CI.Windows.proj

if errorlevel 1 exit /b %ERRORLEVEL%

msbuild /t:pack %* %THIS_DIR%\CI\CI.Windows.proj

if errorlevel 1 exit /b %ERRORLEVEL%

msbuild /t:restore %* %THIS_DIR%\templates\MobileBlazorBindings-template-pack.csproj

if errorlevel 1 exit /b %ERRORLEVEL%

msbuild /t:pack %* %THIS_DIR%\templates\MobileBlazorBindings-template-pack.csproj

if errorlevel 1 exit /b %ERRORLEVEL%

