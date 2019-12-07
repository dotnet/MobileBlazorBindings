using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ShellSectionHandler : ShellGroupItemHandler
    {
        public ShellSectionHandler(NativeComponentRenderer renderer, XF.ShellSection shellSectionControl) : base(renderer, shellSectionControl)
        {
            ShellSectionControl = shellSectionControl ?? throw new ArgumentNullException(nameof(shellSectionControl));
        }

        public XF.ShellSection ShellSectionControl { get; }
    }
}
