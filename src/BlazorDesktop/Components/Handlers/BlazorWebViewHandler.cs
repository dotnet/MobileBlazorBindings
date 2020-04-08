using BlazorDesktop.Elements;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Components.Handlers
{
    public class BlazorWebViewHandler : ViewHandler
    {
        public BlazorWebViewHandler(NativeComponentRenderer renderer, Elements.BlazorWebView control)
            : base(renderer, control)
        {
        }
    }
}
