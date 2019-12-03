@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Emblazon ---
msbuild /t:pack %* %THIS_DIR%\src\Emblazon\Emblazon.csproj
echo --- BlinForms ---
msbuild /t:pack %* %THIS_DIR%\src\BlinForms.Framework\BlinForms.Framework.csproj
echo --- MobileBlazorBindings ---
msbuild /t:pack %* %THIS_DIR%\src\Microsoft.MobileBlazorBindings\Microsoft.MobileBlazorBindings.csproj
echo --- MobileBlazorBindings Templates ---
msbuild /t:pack %* %THIS_DIR%\templates\MobileBlazorBindings-template-pack.csproj
