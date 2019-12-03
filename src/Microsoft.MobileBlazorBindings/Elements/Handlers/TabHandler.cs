using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class TabHandler : ShellSectionHandler
    {
        public TabHandler(EmblazonRenderer renderer, XF.Tab tabControl) : base(renderer, tabControl)
        {
            TabControl = tabControl ?? throw new ArgumentNullException(nameof(tabControl));
        }

        public XF.Tab TabControl { get; }
    }
}
