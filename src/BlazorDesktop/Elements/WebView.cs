using BlazorDesktop.Elements.Handlers;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements
{
    public class WebView : View
    {
        static WebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<WebView>(renderer => new WebViewHandler(renderer, new XF.WebView()));
        }
    }
}
