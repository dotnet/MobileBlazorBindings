using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class NavigableElement : Element
    {
        /// <summary>
        /// A comma-separated list of style classes that should be applied to this element.
        /// </summary>
        [Parameter] public string StyleClass { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (StyleClass != null)
            {
                builder.AddAttribute(nameof(StyleClass), StyleClass);
            }
        }

        public async Task PopModalAsync(bool animated = true)
        {
            await ((NavigableElementHandler)ElementHandler).NavigableElementControl.Navigation.PopModalAsync(animated).ConfigureAwait(true);
        }
    }
}
