$RepoRoot = [System.IO.Path]::GetFullPath("$PSScriptRoot\..\..")
$BuildConfiguration = $env:BUILDCONFIGURATION
if (!$BuildConfiguration) {
    $BuildConfiguration = 'Debug'
}

$PackagesRoot = "$RepoRoot/bin/Packages/$BuildConfiguration"

Write-Host "PackagesRoot = $PackagesRoot"

if (!(Test-Path $PackagesRoot))  { return }

Write-Host "Found folder, looking for files..."

@{
    "$PackagesRoot" = (Get-ChildItem $PackagesRoot -Recurse)
}
