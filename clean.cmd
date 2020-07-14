@echo off
echo +------------------------------------------------------------------------------------------+
echo ^| 0. Cleaning up installed/restored contents of Mobile Blazor Bindings...                  ^|
echo + -----------------------------------------------------------------------------------------+
echo +------------------------------------------------------------------------------------------+
echo ^| 1. Deleting Microsoft.MobileBlazorBindings.* from .nuget folder (some errors are normal) ^|
echo + -----------------------------------------------------------------------------------------+
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings.core
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings.webview
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings.webview.android
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings.webview.ios
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings.webview.macos
rd /s /q %userprofile%\.nuget\packages\microsoft.mobileblazorbindings.webview.windows
echo +----------------------------------------------------------------------------------------+
echo ^| 2. Uninstalling templates (some errors are normal)                                     ^|
echo +----------------------------------------------------------------------------------------+
dotnet new -u Microsoft.MobileBlazorBindings.Templates
