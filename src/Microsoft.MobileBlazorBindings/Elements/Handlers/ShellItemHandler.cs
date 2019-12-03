using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ShellItemHandler : ShellGroupItemHandler
    {
        public ShellItemHandler(EmblazonRenderer renderer, XF.ShellItem shellItemControl) : base(renderer, shellItemControl)
        {
            ShellItemControl = shellItemControl ?? throw new ArgumentNullException(nameof(shellItemControl));
        }

        public XF.ShellItem ShellItemControl { get; }
    }
}
