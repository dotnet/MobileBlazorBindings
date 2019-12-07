using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ShellItemHandler : ShellGroupItemHandler
    {
        public ShellItemHandler(NativeComponentRenderer renderer, XF.ShellItem shellItemControl) : base(renderer, shellItemControl)
        {
            ShellItemControl = shellItemControl ?? throw new ArgumentNullException(nameof(shellItemControl));
        }

        public XF.ShellItem ShellItemControl { get; }
    }
}
