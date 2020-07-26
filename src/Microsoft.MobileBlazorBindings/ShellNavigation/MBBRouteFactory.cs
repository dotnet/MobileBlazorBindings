using Microsoft.MobileBlazorBindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XF = Xamarin.Forms;


namespace MobileBlazorBindingsXaminals.ShellNavigation
{
    //Based on the forms TypeRouteFactory https://github.com/xamarin/Xamarin.Forms/blob/9fd882e6c598a51bffbbb2f4de72c3bd9023ab41/Xamarin.Forms.Core/Routing.cs
    public class MBBRouteFactory : XF.RouteFactory
    {
        readonly Type _type;

        public MBBRouteFactory(Type type)
        {
            _type = type;
        }

        public override XF.Element GetOrCreate()
        {
            //Creates an empty content page, that can safely be returned imediately even if it has no contents.
            var page = new XF.ContentPage();

            //Populates content page. Not ideal to have it in a fire and forget but also not a major issue.
            _ = BuildPage(page);

            return page;
        }

        private async Task BuildPage(XF.ContentPage page)
        {
            //This adds a comoponent to a page and renders the component

            var route = ShellNavigationManager.Current.GetNavigationParameters(_type);

            await ShellNavigationManager.Services.AddComponent(page, _type, route.Parameters).ConfigureAwait(false);
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
