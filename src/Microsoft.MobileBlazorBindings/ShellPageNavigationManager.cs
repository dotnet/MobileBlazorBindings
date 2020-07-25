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

namespace Microsoft.MobileBlazorBindings
{
    public class ShellPageNavigationManager //: NavigationManager I would have liked to inherit from NavigationManager but I can't work out what URIs to initialize it with
    {
        //Flip this to change between Shell.Navigation.Push() and Shell.Goto()
        public bool UseShellRoutes = true;

        static XF.Shell Shell => XF.Shell.Current;
        internal static Dictionary<string, Type> Routes = new Dictionary<string, Type>();

        public static IServiceProvider Services;

        public ShellPageNavigationManager(IServiceProvider services)
        {
            Services = services;
        }



        internal static void FindRoutes(Assembly assembly)
        {
            var pages = assembly.GetTypes().Where(x => x.GetCustomAttributes<RouteAttribute>().Any());//TODO: Could this be more efficient if it only looked for classes that are razor components? Or maybe thats an extra step that would slow things down. Profiler required.
            foreach (var page in pages)
            {
                var routes = page.GetCustomAttributes<RouteAttribute>();
                foreach (var route in routes)
                {
                    //I would like to use the XF.Routes, but can't get them to work with MBBPages
                    if(page.IsSubclassOf(typeof(XF.ContentPage)))
                    {
                        Debug.WriteLine("I have an XF.Page type, this route will work");
                        XF.Routing.RegisterRoute(route.Template, page);
                    }
                    else if(page.IsSubclassOf(typeof(MBB.ContentPage)))
                    {
                        Debug.WriteLine("I have an MBBPAGE type, what now?");
                        XF.Routing.RegisterRoute(route.Template, new MBBRouteFactory(page));

                    }
                    else if(page.IsSubclassOf(typeof(ComponentBase)))
                    {
                        //I have a ComponentBase, which hopefuly contains a ContentView, if so the RouteFactory should work
                        XF.Routing.RegisterRoute(route.Template, new MBBRouteFactory(page));
                    }
                    else
                    {
                        Debug.WriteLine($"What is this {page.Name} {page.BaseType}");// interesting, bears page is a ComponentBase. It's route child is a MBBContentView
                    }



                    //Instead, here's my own dictionary
                    Routes[route.Template] = page;
                }
            }
        }

        [Obsolete("Please use NavigateToAsync if possible. This method is only here for consistency with NavigationManager")]
        public void NavigateTo(string uri)
        {
            _ = NavigateToAsync(uri);
        }


        public Task NavigateToAsync(string uri)
        {
            if (UseShellRoutes)
                return GoToAsync(uri);
            else
                return PushAsync(uri);
            
        }

        private async Task GoToAsync(string uri)
        {

            await Shell.GoToAsync(uri);
        }

        //This version work, but no parameters yet
        //Uses stack navigation which isn't really what I want
        private async Task PushAsync(string uri)
        {
            var type = Routes[uri];

            //I need a service provider
            var page = new XF.ContentPage();
            await Services.AddComponent(page, type);

            await Shell.Navigation.PushAsync(page);
        }

    }

    public static class ShellPageNavigationSupportExtensions
    {
        internal static Type ShellType;
        public static void AddShellPageNavigation(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();

            ShellPageNavigationManager.FindRoutes(assembly);
            //Change to this if we can get ShellPageNavigationManager to extend NavigationManager
            //services.AddSingleton<NavigationManager, ShellPageNavigationManager>();
            services.AddSingleton<ShellPageNavigationManager>();
        }

       
    }

    //Based on the forms TypeRouteFactory https://github.com/xamarin/Xamarin.Forms/blob/9fd882e6c598a51bffbbb2f4de72c3bd9023ab41/Xamarin.Forms.Core/Routing.cs
    public class MBBRouteFactory : RouteFactory
    {
        readonly Type _type;

        public MBBRouteFactory(Type type)
        {
            _type = type;
        }

        public override XF.Element GetOrCreate()
        {
            var page = new XF.ContentPage();
            
            //Fire and forget is not ideal, but atelast this method returns a page instantly, and then adds it's content later.
            _ = ShellPageNavigationManager.Services.AddComponent(page, _type);

            return page;
        }
        public override bool Equals(object obj)
        {
            if ((obj is MBBRouteFactory routeFactory))
                return routeFactory._type == _type;

            return false;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode();
        }
    }
}
