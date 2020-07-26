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
        static XF.Shell Shell => XF.Shell.Current;

        public static ShellNavigationManager Current;

        public static IServiceProvider Services;

        public ShellNavigationManager(IServiceProvider services)
        {
            Services = services;
            Current = this;
        }


        static List<StructuredRoute> Routes = new List<StructuredRoute>();
        internal static void FindRoutes(Assembly assembly)
        {
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
                        //Forms Routing won't be handling parameters.
                        Routing.RegisterRoute(structuredRoute.BaseUri, new MBBRouteFactory(page));
                        
                        //Also register route in our own list for setting parameters and tracking if it is registered;
                        Routes.Add(structuredRoute);
                    }
                    else
                    {
                        Debug.WriteLine($"What is this {page.Name} {page.BaseType}");// interesting, bears page is a ComponentBase. It's route child is a MBBContentView
                        throw new NotImplementedException($"Page directive is invalid on type: {page} because it does not inherit from ComponentBase. Only Components can be used for page routing.");
                    }
                }
            }
        }

        [Obsolete("Please use NavigateToAsync if possible. This method is only here for consistency with NavigationManager")]
        public void NavigateTo(string uri)
        {
            _ = GoToAsync(uri);
        }


        public Task NavigateToAsync(string uri)
        {
            return GoToAsync(uri);
        }

        private async Task GoToAsync(string uri)
        {
            var route = StructuredRoute.FindBestMatch(uri, Routes);

            if (route != null)
            {
                Debug.WriteLine("valid route");
                NavigationParameters[route.Route.Type] = route;

                await Shell.GoToAsync(route.Route.BaseUri);
            }
            else
            {
                throw new InvalidOperationException($"Cannot find route for Navigation. {uri} is not registered, please register it using an @page directive.");
            }

        }


        public Dictionary<Type, StructuredRouteResult> NavigationParameters = new Dictionary<Type, StructuredRouteResult>();
        public StructuredRouteResult GetNavigationParameters(Type page)
        {
            var parameters =  NavigationParameters[page];
            return parameters;
        }


    }

    public static class ShellPageNavigationSupportExtensions
    {
        internal static Type ShellType;
        public static void AddShellPageNavigation(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            ShellNavigationManager.FindRoutes(assembly);
            //Change to this if we can get ShellPageNavigationManager to extend NavigationManager
            //services.AddSingleton<NavigationManager, ShellPageNavigationManager>();
            services.AddSingleton<ShellNavigationManager>();
        }

    }




}
