// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class VerticalStackLayout : StackBase
    {
        static VerticalStackLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<VerticalStackLayout>(
                renderer => new VerticalStackLayoutHandler(renderer, new MC.VerticalStackLayout()));

            RegisterAdditionalHandlers();
        }

        public new MC.VerticalStackLayout NativeControl => ((VerticalStackLayoutHandler)ElementHandler).VerticalStackLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
