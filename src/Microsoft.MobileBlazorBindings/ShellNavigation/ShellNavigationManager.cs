// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Microsoft.MobileBlazorBindings.ShellNavigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings
{
    public class ShellNavigationManager //: NavigationManager I would have liked to inherit from NavigationManager but I can't work out what URIs to initialize it with
    {
        private readonly IServiceProvider _services;
        private readonly List<StructuredRoute> Routes = new List<StructuredRoute>();
        private readonly Dictionary<string, MBBRouteFactory> RouteFactories = new Dictionary<string, MBBRouteFactory>();
        private readonly Dictionary<Type, StructuredRouteResult> NavigationParameters = new Dictionary<Type, StructuredRouteResult>();

        public ShellNavigationManager(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            FindRoutes();
        }

        //TODO This route matching could be better. Can we use the ASPNEt version?
        private void FindRoutes()
        {
            var assembly = XF.Application.Current.GetType().Assembly;
            var pages = assembly.GetTypes().Where(x => x.GetCustomAttributes<RouteAttribute>().Any());//TODO: Could this be more efficient if it only looked for classes that are razor components? Or maybe thats an extra step that would slow things down. Profiler required.
            foreach (var page in pages)
            {
                //Find each @page on a page. There can be multiple.
                var routes = page.GetCustomAttributes<RouteAttribute>();
                foreach (var route in routes)
                {
                    if (route.Template == "/")
                    {
                        // This route can be used in Hybrid apps and should be ignored by Shell (because Shell doesn't support empty routes anyway)
                        continue;
                    }

                    if (page.IsSubclassOf(typeof(ComponentBase)))
                    {
                        var structuredRoute = new StructuredRoute(route.Template, page);

                        //Register with XamarinForms so it can handle Navigation.
                        var routeFactory = new MBBRouteFactory(page, this);
                        XF.Routing.RegisterRoute(structuredRoute.BaseUri, routeFactory);

                        //Also register route in our own list for setting parameters and tracking if it is registered;
                        Routes.Add(structuredRoute);
                        RouteFactories[structuredRoute.BaseUri] = routeFactory;
                    }
                    else
                    {
                        throw new NotImplementedException($"Page directive is invalid on type: {page} because it does not inherit from ComponentBase. Only Components can be used for page routing.");
                    }
                }
            }
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public void NavigateTo(string uri)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            _ = NavigateToAsync(uri);
        }

#pragma warning disable CA1054 // Uri parameters should not be strings
        public async Task NavigateToAsync(string uri)
#pragma warning restore CA1054 // Uri parameters should not be strings
        {
            if (uri is null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            var route = StructuredRoute.FindBestMatch(uri, Routes);

            if (route != null)
            {
                NavigationParameters[route.Route.Type] = route;
                if (!RouteFactories.TryGetValue(route.Route.BaseUri, out var routeFactory))
                {
                    throw new InvalidOperationException($"A route factory for URI '{uri}' could not be found. It should have been registered automatically in the {nameof(ShellNavigationManager)} constructor.");
                }
                await routeFactory.CreateAsync().ConfigureAwait(true);
                await XF.Shell.Current.GoToAsync(route.Route.BaseUri).ConfigureAwait(false);
            }
            else
            {
                throw new InvalidOperationException($"The route '{uri}' is not registered. Register page routes using the '@page' directive in the page.");
            }
        }

        internal async Task<XF.Page> BuildPage(Type componentType)
        {
            var container = new RootContainerHandler();
            var route = NavigationParameters[componentType];

#pragma warning disable CA2000 // Dispose objects before losing scope. Renderer is disposed when page is closed.
            var renderer = new MobileBlazorBindingsRenderer(_services, _services.GetRequiredService<ILoggerFactory>());
#pragma warning restore CA2000 // Dispose objects before losing scope

            var addComponentTask = renderer.AddComponent(componentType, container, route.Parameters);
            var elementAddedTask = container.WaitForElementAsync();

            await Task.WhenAny(addComponentTask, elementAddedTask).ConfigureAwait(false);

            if (container.Elements.Count != 1)
            {
                throw new InvalidOperationException("The target component of a Shell navigation must have exactly one root element.");
            }

            var page = container.Elements.FirstOrDefault() as XF.Page
                ?? throw new InvalidOperationException("The target component of a Shell navigation must derive from the Page component.");

            DisposeRendererWhenPageIsClosed(renderer, page);

            return page;
        }

        private void DisposeRendererWhenPageIsClosed(MobileBlazorBindingsRenderer renderer, XF.Page page)
        {
            // Unfortunately, XF does not expose any Destroyed event for elements.
            // Therefore we subscribe to Navigated event, and consider page as destroyed 
            // if it is not present in the navigation stack.
            XF.Shell.Current.Navigated += DisposeWhenNavigatedAway;

            void DisposeWhenNavigatedAway(object sender, XF.ShellNavigatedEventArgs args)
            {
                // We need to check all navigationStacks for all Shell items.
                var currentPages = XF.Shell.Current.Items
                    .SelectMany(i => i.Items)
                    .SelectMany(i => i.Navigation.NavigationStack);

                if (!currentPages.Contains(page))
                {
                    XF.Shell.Current.Navigated -= DisposeWhenNavigatedAway;
                    renderer.Dispose();
                }
            }
        }
    }
}
