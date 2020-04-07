using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WebWindows.Blazor.XamarinForms
{
    public class BlazorView : ContentView
    {
        private readonly ExtendedWebView _webView = new ExtendedWebView();

        public BlazorView()
        {
            Content = _webView;
            ComponentsDesktop.Run(typeof(MyStartup), _webView, "wwwroot/index.html");
        }

        class MyApp : ComponentBase
        {
            private int count;

            protected override void BuildRenderTree(RenderTreeBuilder builder)
            {
                builder.AddContent(0, "Hi from Blazor with count " + count);
                builder.OpenElement(1, "button");
                builder.AddAttribute(2, "onclick", EventCallback.Factory.Create(this, () => count++));
                builder.AddContent(3, "Increment");
                builder.CloseElement();
            }
        }

        public class MyStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
            }

            public void Configure(DesktopApplicationBuilder app)
            {
                app.AddComponent<MyApp>("app");
            }
        }
    }
}
