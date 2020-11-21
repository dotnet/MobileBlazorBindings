// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using XF = Xamarin.Forms;


namespace Microsoft.MobileBlazorBindings.ShellNavigation
{
    //Based on the forms TypeRouteFactory https://github.com/xamarin/Xamarin.Forms/blob/9fd882e6c598a51bffbbb2f4de72c3bd9023ab41/Xamarin.Forms.Core/Routing.cs
    public class MBBRouteFactory : XF.RouteFactory
    {
        private readonly Type _type;
        private readonly ShellNavigationManager _navigationManager;

        public MBBRouteFactory(Type type, ShellNavigationManager navigationManager)
        {
            _type = type;
            _navigationManager = navigationManager;
        }

        public override XF.Element GetOrCreate()
        {
            return _navigationManager.BuildPage(_type);// new XF.ContentPage();
        }

        public override bool Equals(object obj)
        {
            if ((obj is MBBRouteFactory routeFactory))
            {
                return routeFactory._type == _type;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode();
        }
    }
}
