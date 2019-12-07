using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class Entry : InputView
    {
        static Entry()
        {
            ElementHandlerRegistry.RegisterElementHandler<Entry>(
                renderer => new EntryHandler(renderer, new XF.Entry()));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public string Placeholder { get; set; }

        [Parameter] public EventCallback OnCompleted { get; set; }
        [Parameter] public EventCallback<string> TextChanged { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Placeholder != null)
            {
                builder.AddAttribute(nameof(Placeholder), Placeholder);
            }

            builder.AddAttribute("oncompleted", OnCompleted);
            builder.AddAttribute("ontextchanged", EventCallback.Factory.Create<ChangeEventArgs>(this, HandleTextChanged));
        }

        private Task HandleTextChanged(ChangeEventArgs evt)
        {
            return TextChanged.InvokeAsync((string)evt.Value);
        }
    }
}
