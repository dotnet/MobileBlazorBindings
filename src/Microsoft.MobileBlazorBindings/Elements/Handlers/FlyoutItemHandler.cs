using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class FlyoutItemHandler : ShellItemHandler
    {
        public FlyoutItemHandler(EmblazonRenderer renderer, XF.FlyoutItem flyoutItemControl) : base(renderer, flyoutItemControl)
        {
            FlyoutItemControl = flyoutItemControl ?? throw new ArgumentNullException(nameof(flyoutItemControl));
        }

        public XF.FlyoutItem FlyoutItemControl { get; }
    }
}
