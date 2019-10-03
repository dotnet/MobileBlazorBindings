$RepoRoot = [System.IO.Path]::GetFullPath("$PSScriptRoot\..\..")
$BuildConfiguration = $env:BUILDCONFIGURATION

Write-Host "BuildConfiguration(1) = $BuildConfiguration"

if (!$BuildConfiguration) {
    $BuildConfiguration = 'Debug'
}

Write-Host "BuildConfiguration(2) = $BuildConfiguration"

$PackagesRoot = "$RepoRoot/bin/Packages/$BuildConfiguration"

Write-Host "PackagesRoot = $PackagesRoot"

if (!(Test-Path $PackagesRoot))  { return }

Write-Host "Found folder, looking for files..."

@{
    "$PackagesRoot" = (Get-ChildItem $PackagesRoot -Recurse)
}
