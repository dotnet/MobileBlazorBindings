# Experimental Mobile Blazor Bindings

Mobile Blazor Bindings enable developers to build native mobile apps using C# and .NET for iOS and Android using familiar web programming patterns. This means you can use the Blazor programming model and Razor syntax to define UI components and behaviors of an application. The UI components that are included are based on Xamarin.Forms native UI controls, which results in beautiful native mobile apps.

Here is a sample Counter component, which may look familiar to Blazor developers, that increments a value on each button press:

```xml
<StackLayout>
    <Label FontSize="30">You pressed @count times </Label>
    <Button Text="+1" OnClick="@HandleClick" />
</StackLayout>

@code {
    int count;

    void HandleClick()
    {
        count++;
    }
}
```

Notice that the Blazor model is present with code sitting side by side the user interface markup that leverages Razor syntax with mobile specific components. This will feel very natural for any web developer that has ever used Razor syntax in the past. Now with the Experimental Mobile Blazor Bindings you can leverage your existing web skills and knowledge to build native iOS and Android apps powered by .NET.

Here is the code above running in the Android Emulator:

<img src="https://devblogs.microsoft.com/aspnet/wp-content/uploads/sites/16/2020/01/blazor-android-counter-2.gif" alt="Clicking increment button in Android emulator" width="300" height="533" class="aligncenter size-full wp-image-23061" />

## Get started with Mobile Blazor Bindings

To get started, all you need is the [.NET Core 3.0 or 3.1 SDK][1], Visual Studio or Visual Studio for Mac, and the ASP.NET and web development and Mobile development with .NET (Xamarin.Forms) workloads installed.

Get started by reading the Getting Started section in the [docs](https://docs.microsoft.com/mobile-blazor-bindings/) and read through the related walkthroughs.


## Contributing

As an experimental project, there are several active areas of development and we're looking for your feedback to help set the direction for this project. Please check it out and let us know any feedback you have on the project by logging issues in this repo.

Thank you!

 [1]: https://dotnet.microsoft.com/download
