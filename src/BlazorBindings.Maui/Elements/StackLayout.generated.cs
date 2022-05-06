// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class StackLayout : StackBase
    {
        static StackLayout()
        {
            ElementHandlerRegistry.RegisterElementHandler<StackLayout>(
                renderer => new StackLayoutHandler(renderer, new MC.StackLayout()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.StackOrientation? Orientation { get; set; }

        public new MC.StackLayout NativeControl => ((StackLayoutHandler)ElementHandler).StackLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
