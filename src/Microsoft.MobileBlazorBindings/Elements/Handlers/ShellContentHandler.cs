using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ShellContentHandler : BaseShellItemHandler
    {
        public ShellContentHandler(EmblazonRenderer renderer, XF.ShellContent shellContentControl) : base(renderer, shellContentControl)
        {
            ShellContentControl = shellContentControl ?? throw new ArgumentNullException(nameof(shellContentControl));
        }

        public XF.ShellContent ShellContentControl { get; }
    }
}
