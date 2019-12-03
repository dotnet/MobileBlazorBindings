using Emblazon;
using Microsoft.AspNetCore.Components;

namespace Microsoft.Blazor.Native.Elements
{
    public abstract class Element : NativeControlComponentBase
    {
        [Parameter] public string StyleId { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (StyleId != null)
            {
                builder.AddAttribute(nameof(StyleId), StyleId);
            }
        }
    }
}
