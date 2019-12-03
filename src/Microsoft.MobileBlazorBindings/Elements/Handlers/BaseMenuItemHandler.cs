using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public abstract class BaseMenuItemHandler : ElementHandler
    {
        public BaseMenuItemHandler(EmblazonRenderer renderer, XF.BaseMenuItem baseMenuItemControl) : base(renderer, baseMenuItemControl)
        {
            BaseMenuItemControl = baseMenuItemControl ?? throw new ArgumentNullException(nameof(baseMenuItemControl));
        }

        public XF.BaseMenuItem BaseMenuItemControl { get; }
    }
}
