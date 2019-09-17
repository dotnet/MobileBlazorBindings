using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Emblazon
{
    public abstract class NativeControlComponentBase : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.OpenElement(0, GetType().FullName);
            RenderAttributes(new AttributesBuilder(builder));

            var childContent = GetChildContent();
            if (childContent != null)
            {
                builder.AddContent(2, childContent);
            }

            builder.CloseElement();
        }

        protected virtual void RenderAttributes(AttributesBuilder builder)
        {
        }

        protected virtual RenderFragment GetChildContent() => null;
    }
}
