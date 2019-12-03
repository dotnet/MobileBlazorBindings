using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ShellSectionHandler : ShellGroupItemHandler
    {
        public ShellSectionHandler(EmblazonRenderer renderer, XF.ShellSection shellSectionControl) : base(renderer, shellSectionControl)
        {
            ShellSectionControl = shellSectionControl ?? throw new ArgumentNullException(nameof(shellSectionControl));
        }

        public XF.ShellSection ShellSectionControl { get; }
    }
}
