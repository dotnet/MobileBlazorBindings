// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Microsoft.MobileBlazorBindings.Hosting;
using Microsoft.MobileBlazorBindings.WebView.Elements;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class BlazorWebView : View
    {
        static BlazorWebView()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<BlazorWebView>(renderer => new BlazorWebViewHandler(renderer, new MobileBlazorBindingsBlazorWebView(renderer.Dispatcher)));
        }

        [Inject] internal IHost Host { get; private set; }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        /// <summary>
        /// Gets or sets the ErrorHandler that will be used to catch unhandled exceptions.
        /// </summary>
        [Parameter] public IBlazorErrorHandler ErrorHandler { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var element = (MobileBlazorBindingsBlazorWebView)NativeControl;

            if (firstRender)
            {
                element.Host = Host;
                // The rerender is triggered whenever Blazor has to reload e.g. when navigating away from a page
                // and subsequently coming back to the app URL. 
                element.RerenderAction = () => InvokeAsync(() => StateHasChanged());

                if (ErrorHandler != null)
                {
                    element.ErrorHandler = ErrorHandler;
                }

                element.SetInitialSource();
            }
            else
            {
                await element.Render(ChildContent).ConfigureAwait(false);
            }
        }
    }
}
