using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class FormattedString : Element
    {
        static FormattedString()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<FormattedString>(renderer => new FormattedStringHandler(renderer, new XF.FormattedString()));
        }

        [Parameter] public RenderFragment Spans { get; set; }

        protected override RenderFragment GetChildContent() => Spans;
    }
}
