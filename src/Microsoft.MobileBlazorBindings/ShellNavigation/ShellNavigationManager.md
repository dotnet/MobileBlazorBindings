//Draft of Shell Navigation Manager Docs.
//This should be enough to get someone up and running
//I'm happy to write it out in full and add screen shots but leaving it pretty rough until I know where it will end up.
 

# Shell Navigation Manager

Shell Navigation Manager provides route based navigation within a Mobile Blazor Bindings App. The API is designed to feel familiar for Blazor developers with routes added using the `@page`, access via dependecy injection and and routes with parameters in the same formats
Internally it is implemented using Xamarin Forms Shell URI Navigation. This allows you to use Shell to handle flyout drawers or tab navigation for the core of your app and route navigation within pages. 

For more details on Shell check out the [Xamarin Forms documentation](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/shell/), Details of routing in Blazor are in the [ASP.NET Core documentation](https://docs.microsoft.com/en-us/aspnet/core/blazor/fundamentals/routing?view=aspnetcore-3.1)

For a sample of ShellNavigationManager see the Xaminals Sample in the MobileBlazorBindings repo.

## Setup
To use the ShellNavigationManager you'll need to have a Shell as the MainPage in your app.

To start off here is a fairly simple Shell which will give us two tabs for a HomePage and an AboutPage. This should be in a file called `AppShell.razor`.

```
<Shell FlyoutBehavior="FlyoutBehavior.Disabled">
    <TabBar>
        <Tab>
            <ShellContent>
                <HomePage />
            </ShellContent>

            <ShellContent>
                <AboutPage />
            </ShellContent>
        </Tab>
    </TabBar>
</Shell>
```

*Note*: The HomePage and AboutPage must have *ContentPage* as their root element ot be used in Shell.



Inside App.cs, your AppShell needs to be set as the MainPage of your app. This is done by calling AddComponent. Setting the Shell as the MainPage is done in the background so we also have to set a blank ContentPage as a placeholder while it is loading.

To enable ShellNavigationManager and make it available for our pages we add it as a singleton service.

This should give you an App constructor something along the lines of:

```
public App()
{
    AppHost = MobileBlazorBindingsHost.CreateDefaultBuilder()
        .ConfigureServices((hostContext, services) =>
        {
            // Register app-specific services
            //services.AddSingleton<AppState>();
            services.AddSingleton<ShellNavigationManager>();
        })
        .Build();

    MainPage = new ContentPage();
    AppHost.AddComponent<AppShell>(parent: this);
}
```

## Registering Routes
Route registration occurs in each razor component that you want to be able to navigate to using the `@page` directive followed by a string Uri. This Uri must start with a slash.

For example here's a contact page with the route "/contact"

```
@page "/contact"
<ContentView>
    <StackLayout>
        <Label Text="Phone Number:"></Label>
        <Label Text="123456"></Label>
    </StackLayout>
</ContentView>
```

*Note*: At present target pages must have a *ContentView* as their root element. This differs from the *ContentPage* which must be used for children in a Shell.

## Navigating to a page
Navigating to a page is achived with an instance of the ShellNavigationManager which you can access in your components using the `@inject` directive. Once you have an instance you call NavigateToAsyn() with the Uri for the page you want to open.

For example in my AboutPage I want to navigate to the ContactPage.

```
@inject ShellNavigationManager NavigationManager
<ContentPage>
    <Stack
<ContentPage>
```