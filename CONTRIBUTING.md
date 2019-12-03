# Contributing

This project has adopted the [Microsoft Open Source Code of
Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct
FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com)
with any additional questions or comments.

## Best practices

* Use Windows PowerShell or [PowerShell Core][pwsh] (including on Linux/OSX) to run .ps1 scripts.
  Some scripts set environment variables to help you, but they are only retained if you use PowerShell as your shell.

## Prerequisites

To build, test, and deploy from this repository you need the [.NET Core SDK](https://get.dot.net/).
You should install the version specified in `global.json` or a later version within the same major.minor.Bxx "hundreds" band.
For example if 2.2.300 is specified, you may install 2.2.300, 2.2.301, or 2.2.310, while the 2.2.400 version would not be considered compatible by .NET SDK.
See [.NET Core Versioning](https://docs.microsoft.com/en-us/dotnet/core/versions/) for more information.

All dependencies can be installed by running the `init.ps1` script at the root of the repository using Windows PowerShell or [PowerShell Core][pwsh] (on any OS).

## Package restore

The easiest way to restore packages may be to run `init.ps1` which automatically authenticates to the feeds that packages for this repo come from, if any.
`dotnet restore` or `nuget restore` also work.

## Building

You can build from the command line using MSBuild or from within Visual Studio.


[pwsh]: https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell?view=powershell-6
