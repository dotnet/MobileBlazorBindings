using Microsoft.AspNetCore.Components;

namespace BlazorDesktop.Elements
{
    public class MobileBlazorBindingsBlazorWebView : BlazorWebView
    {
        public MobileBlazorBindingsBlazorWebView(Dispatcher dispatcher)
            : base(dispatcher, initOnParentSet: false)
        {
        }
    }
}
