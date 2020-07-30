# Known issues with ShellNavigationManager

There are a handful off issues with the ShellNavigationManager, for now we're saying it's good enough for an experimental project, but these will all need to be investigated later. 
Some of them may require changes inside ASP.NET Core or Xamarin.Forms, others may just need more research.

## The two overloads of AddComponent in MobileBlazorBindingsHostExtensions could share more code

## Find Routes
Upon constuction ShellNavigationManager calls FindRoutes, which scans the assembly for routes declared with the @page directive, and it registers the routes with Shell for navigation and keeps its own list of routes for setting parameters when shell navigates.

This call has a few issues:
* Assembly scanning can be very slow and this call is on the critical path for app startup
* It does not handle multiple routes with the same form but different argument types
* There must be something like this in ASP.NET CORE already does this, it's probably more efficient, handles all the different parameter types and route forms better and is already stable, tested and mature.

## NavigateTo - find best match
When you navigate with a URL, we try to match it with the best match from the registered route.
* This find best match does not have support for different argument types.
* This is probably duplicating code that exists in ASP.NET Core.

## Shell

Shell Navigation routes can do lots of things and I've only tested it for a few of the most common scenarios. I think most of the route navigation features will work.

## NavigationManager inheritance
In ASP.NET Core there in an AbstractClass called NavigationManager, it has various navigation managers that extend it depending on how it's being used. Ideally the ShellNavigatationManager should extend this too but when I try I can't get it to initialize because it doesn't like my URIs.

## ContentViews instead of ContentPages as Navigation Targets
When navigating the target page needs to have a content view as the only child, which will then be wrapped in a ContentPage by the ShellNavigationManager. It would make more sense to have the ContentPage declared in each component.


## Component Extensions
We have two fairly similar methods here, we can probably refactor things a little nicer to prevent code duplication but I didn't want to rip apart a method that is used in lots of places.

## NavigateTo vs NavigateToAsync
Shell navigation is asynchronous so the ShellNavigationManager exposes the method `NavigateToAsyn`. This is at odds with the ASP.NET CORE Navigation 