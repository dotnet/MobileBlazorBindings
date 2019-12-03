using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class TabBarHandler : ShellItemHandler
    {
        public TabBarHandler(EmblazonRenderer renderer, XF.TabBar tabBarControl) : base(renderer, tabBarControl)
        {
            TabBarControl = tabBarControl ?? throw new ArgumentNullException(nameof(tabBarControl));
        }

        public XF.TabBar TabBarControl { get; }
    }
}
