using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public abstract class FormsComponentBase : ComponentBase
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, GetType().FullName);

            RenderAttributes(builder);

            RenderContents(builder);

            builder.CloseElement();
        }

        /// <summary>
        /// Rendered attributes should use sequence values 1-99.
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void RenderAttributes(RenderTreeBuilder builder)
        {
        }

        /// <summary>
        /// Rendered contents should use sequence values 1000+.
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void RenderContents(RenderTreeBuilder builder)
        {
        }

        public static void ApplyAttribute(Element control, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"FormsComponentBase doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
