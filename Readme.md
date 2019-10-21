# Emblazon - Home of Blazor Native

[![Build Status](https://devdiv.visualstudio.com/Personal/_apis/build/status/xamarin.Emblazon?branchName=master)](https://devdiv.visualstudio.com/Personal/_build/latest?definitionId=12095&branchName=master)

## What is Emblazon?

Emblazon enables using Blazor to target native UI renderers (as opposed to web/HTML renderers). This repo contains implementations that target Xamarin.Forms and Windows Forms.


## Start Emblazoning!

### Required software to use Emblazon and for contributors

Emblazon and its related projects require:

1. [Visual Studio 2019 16.3](https://visualstudio.microsoft.com/vs/) or newer
   * For Blazor Native, the Xamarin workload must be enabled in Visual Studio
   * For BlinForms, you must run on Windows (that's the `in` of BlinForms!)
2. [.NET Core 3.0 SDK](https://dotnet.microsoft.com/download)


### Create a Blazor Native project

1. In Visual Studio, create a Xamarin.Forms Mobile App project
1. Select the Blank app template
1. Add the Emblazon NuGet package feed by adding a new `NuGet.config` file to the solution directory:

    ```xml
    <?xml version="1.0" encoding="utf-8"?>
    <configuration>
        <packageSources>
        <add key="Emblazon Feed" value="https://devdiv.pkgs.visualstudio.com/_packaging/Emblazon/nuget/v3/index.json" />
        </packageSources>
    </configuration>
    ```

1. In the solution's shared project (the one that isn't Android or iOS), make these changes:
   1. Change the top `<Project>` node to use the Razor SDK `<Project Sdk="Microsoft.NET.Sdk.Razor">`
   1. In the `<PropertyGroup>` section, add this node to set the Razor language version: `<RazorLangVersion>3.0</RazorLangVersion>`
   1. Add a reference to Blazor Native in the `<ItemGroup>` containing other package references: `<PackageReference Include="Microsoft.Blazor.Native" Version="0.1.71-beta" />` (update to a newer build as needed)
   1. Delete `MainPage.xaml` and `MainPage.xaml.cs`
   1. Delete `App.xaml` and `App.xaml.cs`
   1. Add a new class file called `App.cs` to the project
   1. Add these `using` statements:

   ```c#
   using Microsoft.Blazor.Native;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.Hosting;
   using Xamarin.Forms;
   ```

   1. Replace the app class's code with this code:

   ```c#
    public class App : Application
    {
        public App()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Register app-specific services
                })
                .Build();

            host.Services.AddComponent<MainPage>(parent: this);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
   ```

   1. Add a Razor Component to the project named `_Imports.razor` with these contents:

   ```c#
    @using Microsoft.Blazor.Native.Elements
    @using Xamarin.Forms
   ```

   1. Add a Razor Component to the project named `MainPage.razor`

   ```html
    <ContentPage>
        <StackLayout>
            <Button Text="Add!" OnClick="@OnAdd" />
            <Label Text="@($"The button has been clicked {counter} times!")" />
        </StackLayout>
    </ContentPage>

    @code {
        int counter;

        void OnAdd()
        {
            counter++;
        }
    }
   ```
1. You're ready to go! Run the Android or iOS project to launch your new Blazor Native-based app!


## NuGet feed

Browse the latest packages here: https://devdiv.visualstudio.com/Personal/_packaging?_a=feed&feed=Emblazon


## Solution structure

There are 3 main areas:

1. [Emblazon](src/Emblazon) - Base framework for Blazor rendering to native UI frameworks.
2. [BlinForms](src/BlinForms.Framework) - Blazor rendering to Windows Forms.
   * Check out the [BlinForms sample app](samples/BlinFormsSample).
3. [Microsoft.Blazor.Native](src/Microsoft.Blazor.Native) - Blazor rendering to Xamarin.Forms.
   * Check out the [Blazor Native sample todo app](samples/BlazorNativeTodo).


## Comparison projects

To compare Blazor Native with Xamarin.Forms, see the [Xamarin.Forms Todo XAML sample](samples/XamarinFormsTodoXaml/XamarinTodoXaml/XamarinTodoXaml).

