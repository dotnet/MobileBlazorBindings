using System;
using System.Collections.Generic;
using System.Text;
using XF = Xamarin.Forms;
using MBB = Microsoft.MobileBlazorBindings;
using Xamarin.Forms;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings
{

    //This class was mainly for testing some concepts. It's not fully implemented but it's not far off.
    //Shell Navigation is the main focus
    public class PageNavigationManager
    {
        private IServiceProvider _services;

        public PageNavigationManager(IServiceProvider serviceProvider)
        {
            _services = serviceProvider;
        }

        public async Task NavigatToAsync<T>(IHost host)  where T : IComponent
        {
            var app = XF.Application.Current;
            var mainPage = app.MainPage;

            if (!(mainPage is NavigationPage nav))
            {
                throw new InvalidOperationException($"Applications Main Page is not a Navigation Page so PageNavigationManager cannot me used. Please wrap your ${mainPage.GetType()} in a NavigationPage");
            }


            var page = new ContentPage();
            //This one is broken even though in theory it is better
            //It displays the page once but can't be updated
            await host.Services.AddComponent(page, typeof(T));
            
            // This one works but doesn't return a component and has the bad async in it
            //host.AddComponent<T>(page);

            nav.PushAsync(page);
        }


        public async Task NavigateToAsync(Type type)
        {
            if(!type.IsSubclassOf(typeof(Microsoft.AspNetCore.Components.ComponentBase)))
            {
                throw new InvalidOperationException($"Cannot navigate to {type.FullName} because it is not a ComponentBase.");
            }

            var app = XF.Application.Current;
            var mainPage = app.MainPage;

            if (!(mainPage is NavigationPage nav))
            {
                throw new InvalidOperationException($"Applications Main Page is not a Navigation Page so PageNavigationManager cannot me used. Please wrap your ${mainPage.GetType()} in a NavigationPage");
            }

            var page = new ContentPage();
            _services.AddComponent(page, type);


            await nav.PushAsync(page);

        }
    }
}
