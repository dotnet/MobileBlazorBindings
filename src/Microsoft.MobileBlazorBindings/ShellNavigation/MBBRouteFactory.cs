// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

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
        readonly ShellNavigationManager _navigationManager;

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
                return routeFactory._type == _type;

            return false;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode();
        }
    }
}
