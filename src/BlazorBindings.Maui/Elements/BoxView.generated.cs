// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class BoxView : View
    {
        static BoxView()
        {
            ElementHandlerRegistry.RegisterElementHandler<BoxView>(
                renderer => new BoxViewHandler(renderer, new MC.BoxView()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public CornerRadius? CornerRadius { get; set; }

        public new MC.BoxView NativeControl => ((BoxViewHandler)ElementHandler).BoxViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.CornerRadiusToString(CornerRadius.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
