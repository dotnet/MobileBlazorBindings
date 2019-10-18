using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
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
