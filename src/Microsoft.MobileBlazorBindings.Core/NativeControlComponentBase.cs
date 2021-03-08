// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;

namespace Microsoft.MobileBlazorBindings.Core
{
    public abstract class NativeControlComponentBase : ComponentBase
    {
        public IElementHandler ElementHandler { get; private set; }

        public void SetElementReference(IElementHandler elementHandler)
        {
            ElementHandler = elementHandler ?? throw new ArgumentNullException(nameof(elementHandler));
        }

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

            int sequence = 3;
            RenderAdditionalElementContent(builder, ref sequence);

            builder.CloseElement();
        }

        protected virtual void RenderAttributes(AttributesBuilder builder)
        {
        }

        protected virtual void RenderAdditionalElementContent(RenderTreeBuilder builder, ref int sequence)
        {
        }

        protected virtual RenderFragment GetChildContent() => null;
    }
}
