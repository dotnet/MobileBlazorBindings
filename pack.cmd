@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Microsoft.MobileBlazorBindings.Core ---
msbuild /t:pack %* %THIS_DIR%\src\Microsoft.MobileBlazorBindings.Core\Microsoft.MobileBlazorBindings.Core.csproj
echo --- BlinForms ---
msbuild /t:pack %* %THIS_DIR%\src\BlinForms.Framework\BlinForms.Framework.csproj
echo --- MobileBlazorBindings ---
msbuild /t:pack %* %THIS_DIR%\src\Microsoft.MobileBlazorBindings\Microsoft.MobileBlazorBindings.csproj
echo --- MobileBlazorBindings Templates ---
msbuild /t:pack %* %THIS_DIR%\templates\MobileBlazorBindings-template-pack.csproj
