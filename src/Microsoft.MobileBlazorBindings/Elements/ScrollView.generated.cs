// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ScrollView : Layout
    {
        static ScrollView()
        {
            ElementHandlerRegistry.RegisterElementHandler<ScrollView>(
                renderer => new ScrollViewHandler(renderer, new XF.ScrollView()));
        }

        [Parameter] public XF.ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public XF.ScrollOrientation? Orientation { get; set; }
        [Parameter] public XF.ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        public new XF.ScrollView NativeControl => ((ScrollViewHandler)ElementHandler).ScrollViewControl;

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
    }
}
