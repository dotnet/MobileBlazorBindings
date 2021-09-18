using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.DataTemplates
{
#pragma warning disable CA1812 // Avoid uninstantiated internal classes. Class is used as generic parameter.
    internal class InitializedContentView : ContentView
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
    {
        [Parameter] public new XF.ContentView NativeControl { get; set; }

        static InitializedContentView()
        {
            ElementHandlerRegistry.RegisterElementHandler<InitializedContentView>(
                (renderer, _, component) => new ContentViewHandler(renderer, component.NativeControl));
        }
    }
}
