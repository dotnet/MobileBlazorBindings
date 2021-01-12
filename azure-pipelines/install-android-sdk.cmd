@echo off
type yes.txt | "C:\Program Files (x86)\Android\android-sdk\tools\bin\sdkmanager.bat" --licenses
type yes.txt | "C:\Program Files (x86)\Android\android-sdk\tools\bin\sdkmanager.bat" "platforms;android-29"
