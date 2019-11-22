@echo off
setlocal

set THIS_DIR=%~dp0

echo --- Emblazon ---
msbuild /t:pack %* %THIS_DIR%\src\Emblazon\Emblazon.csproj
echo --- BlinForms ---
msbuild /t:pack %* %THIS_DIR%\src\BlinForms.Framework\BlinForms.Framework.csproj
echo --- Blazor Native ---
msbuild /t:pack %* %THIS_DIR%\src\Microsoft.Blazor.Native\Microsoft.Blazor.Native.csproj
echo --- Blazor Native Templates ---
msbuild /t:pack %* %THIS_DIR%\templates\blazor-native-template-pack.csproj
