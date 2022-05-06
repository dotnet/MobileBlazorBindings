// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class ScrollView : BlazorBindings.Maui.Elements.Compatibility.Layout
    {
        static ScrollView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ScrollView>(
                renderer => new ScrollViewHandler(renderer, new MC.ScrollView()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public ScrollOrientation? Orientation { get; set; }
        [Parameter] public ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        public new MC.ScrollView NativeControl => ((ScrollViewHandler)ElementHandler).ScrollViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(HorizontalScrollBarVisibility), (int)HorizontalScrollBarVisibility.Value);
            }
            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (VerticalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(VerticalScrollBarVisibility), (int)VerticalScrollBarVisibility.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
