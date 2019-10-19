using Microsoft.Blazor.Native.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class ContentView : TemplatedView
    {
        static ContentView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ContentView>(
                renderer => new ContentViewHandler(renderer, new XF.ContentView()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
