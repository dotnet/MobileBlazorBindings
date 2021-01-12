@echo off
type "%~dp0\yes.txt" | "C:\Program Files (x86)\Android\android-sdk\tools\bin\sdkmanager.bat" --licenses
type "%~dp0\yes.txt" | "C:\Program Files (x86)\Android\android-sdk\tools\bin\sdkmanager.bat" "platforms;android-29"
