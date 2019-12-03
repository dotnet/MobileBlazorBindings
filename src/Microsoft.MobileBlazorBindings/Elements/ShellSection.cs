using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.Blazor.Native.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class ShellSection : ShellGroupItem
    {
        static ShellSection()
        {
            ElementHandlerRegistry.RegisterElementHandler<ShellSection>(
                renderer => new ShellSectionHandler(renderer, new XF.ShellSection()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
