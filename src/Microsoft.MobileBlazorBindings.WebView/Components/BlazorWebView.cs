using Microsoft.MobileBlazorBindings.WebView.Components.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.WebView.Components
{
    public class BlazorWebView : View
    {
        static BlazorWebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<BlazorWebView>(renderer => new BlazorWebViewHandler(renderer, new Elements.MobileBlazorBindingsBlazorWebView(renderer.Dispatcher)));
        }

        [Inject] internal IServiceProvider Services { get; private set; }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public string ContentRoot { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var element = (Elements.MobileBlazorBindingsBlazorWebView)NativeControl;

            if (firstRender)
            {
                element.ContentRoot = ContentRoot;
                element.Services = Services;
                await element.InitAsync();
            }

            element.Render(ChildContent);
        }
    }
}
