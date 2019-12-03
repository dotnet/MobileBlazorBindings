using Emblazon;
using Microsoft.AspNetCore.Components;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class InputView : View
    {
        [Parameter] public bool? IsReadOnly { get; set; }
        [Parameter] public bool? IsSpellCheckEnabled { get; set; }
        //[Parameter] public XF.Keyboard Keyboard { get; set; }
        [Parameter] public int? MaxLength { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsReadOnly != null)
            {
                builder.AddAttribute(nameof(IsReadOnly), IsReadOnly);
            }
            if (IsSpellCheckEnabled != null)
            {
                builder.AddAttribute(nameof(IsSpellCheckEnabled), IsSpellCheckEnabled);
            }
            if (MaxLength != null)
            {
                builder.AddAttribute(nameof(MaxLength), MaxLength);
            }
        }
    }
}
