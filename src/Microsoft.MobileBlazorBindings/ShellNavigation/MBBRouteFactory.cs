// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.ShellNavigation
{
    //Based on the forms TypeRouteFactory https://github.com/xamarin/Xamarin.Forms/blob/9fd882e6c598a51bffbbb2f4de72c3bd9023ab41/Xamarin.Forms.Core/Routing.cs
    public class MBBRouteFactory : XF.RouteFactory
    {
        private readonly Type _componentType;
        private readonly ShellNavigationManager _navigationManager;
        private XF.Element _element;

        public MBBRouteFactory(Type componentType, ShellNavigationManager navigationManager)
        {
            _componentType = componentType ?? throw new ArgumentNullException(nameof(componentType));
            _navigationManager = navigationManager ?? throw new ArgumentNullException(nameof(navigationManager));
        }

        public override XF.Element GetOrCreate()
        {
            return _element
                ?? throw new InvalidOperationException("The target element of the Shell navigation is supposed to be created at this point.");
        }

        public async Task CreateAsync()
        {
            _element = await _navigationManager.BuildPage(_componentType).ConfigureAwait(false);
        }

        public override bool Equals(object obj)
        {
            if ((obj is MBBRouteFactory otherRouteFactory))
            {
                return otherRouteFactory._componentType == _componentType;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return _componentType.GetHashCode();
        }
    }
}
