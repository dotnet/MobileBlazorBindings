using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ShellContentHandler : BaseShellItemHandler
    {
        public ShellContentHandler(NativeComponentRenderer renderer, XF.ShellContent shellContentControl) : base(renderer, shellContentControl)
        {
            ShellContentControl = shellContentControl ?? throw new ArgumentNullException(nameof(shellContentControl));
        }

        public XF.ShellContent ShellContentControl { get; }
    }
}
