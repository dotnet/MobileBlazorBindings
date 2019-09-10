# Emblazon - Home of Blaxamarin and BlinForms

## What is Emblazon?

Emblazon enables using Blazor to target native UI renderers (as opposed to web renderers). This repo contains implementations that target Windows Forms and Xamarin.Forms.


## Required software

Emblazon and its related projects require:

1. [Visual Studio 2019 Preview](https://visualstudio.microsoft.com/vs/preview/)
  * For Blaxamarin, the Xamarin workload must be enabled in Visual Studio
  * For BlinForms, you must run on Windows (that's the `in` of BlinForms!)
2. [.NET Core 3.0 Preview SDK](https://dotnet.microsoft.com/download/dotnet-core/3.0)


## Project structure

There are 3 main areas:
1. [Emblazon](Emblazon) - Base framework for Blazor rendering to native UI frameworks.
2. [BlinForms](BlinForms.Framework) - Blazor rendering to WinForms.
   - Check out the [BlinForms sample app](BlinFormsSample).
3. [Blaxamarin](Blaxamarin.Framework) - Blazor rendering to Xamarin.Forms.
   - Check out the [Blaxamarin sample app](BlaxamarinSample).
