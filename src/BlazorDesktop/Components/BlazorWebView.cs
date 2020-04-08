using BlazorDesktop.Components.Handlers;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorDesktop.Components
{
    public class BlazorWebView : View
    {
        static BlazorWebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<BlazorWebView>(renderer => new BlazorWebViewHandler(renderer, new Elements.BlazorWebView(renderer.Dispatcher)));
        }

        [Inject] internal IServiceProvider Services { get; private set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var element = (Elements.BlazorWebView)NativeControl;

            if (firstRender)
            {
                await element.InitAsync(Services);
            }

            element.Render(ChildContent);
        }
    }
}
