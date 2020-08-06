// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Extensions.Hosting;
using MBB = Microsoft.MobileBlazorBindings.Elements;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF = Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements;
using Xamarin.Forms;
using System.Diagnostics;
using MobileBlazorBindingsXaminals.ShellNavigation;

namespace Microsoft.MobileBlazorBindings
{
    public class ShellNavigationManager //: NavigationManager I would have liked to inherit from NavigationManager but I can't work out what URIs to initialize it with
    {
        IServiceProvider _services;
        List<StructuredRoute> Routes = new List<StructuredRoute>();
        Dictionary<Type, StructuredRouteResult> NavigationParameters = new Dictionary<Type, StructuredRouteResult>();

        public ShellNavigationManager(IServiceProvider services)
        {
            _services = services;
            FindRoutes();
        }


        //TODO This route matching could be better. Can we use the ASPNEt version?
        void FindRoutes()
        {
            var assembly = XF.Application.Current.GetType().Assembly;
            var pages = assembly.GetTypes().Where(x => x.GetCustomAttributes<RouteAttribute>().Any());//TODO: Could this be more efficient if it only looked for classes that are razor components? Or maybe thats an extra step that would slow things down. Profiler required.
            foreach (var page in pages)
            {
                //Find each @page on a page. There can be multiple.
                var routes = page.GetCustomAttributes<RouteAttribute>();
                foreach (var route in routes)
                {
                    if (page.IsSubclassOf(typeof(ComponentBase)))
                    {
                        var structuredRoute = new StructuredRoute(route.Template, page);

                        //Register with XamarinForms so it can handle Navigation.
                        Routing.RegisterRoute(structuredRoute.BaseUri, new MBBRouteFactory(page, this));
                        
                        //Also register route in our own list for setting parameters and tracking if it is registered;
                        Routes.Add(structuredRoute);
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

                await XF.Shell.Current.GoToAsync(route.Route.BaseUri).ConfigureAwait(false) ;
            }
            else
            {
                throw new InvalidOperationException($"Cannot find route for Navigation. {uri} is not registered, please register it using an @page directive.");
            }
        }


        internal XF.ContentPage BuildPage(Type type)
        {
            var page = new XF.ContentPage();
            //Fire and forget is not ideal, could consider a Task.Wait but that's probably worse.
            _ = PopulatePage(page, type);
            return page;
        }

        private async Task PopulatePage(XF.ContentPage page , Type type)
        {
            var route = NavigationParameters[type];
            await _services.AddComponent(page, type, route.Parameters).ConfigureAwait(false);
        }
    }

}
