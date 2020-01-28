// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class StackLayout : Layout
    {
        static StackLayout()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<StackLayout>(renderer => new StackLayoutHandler(renderer, new XF.StackLayout()));
        }

        [Parameter] public XF.StackOrientation? Orientation { get; set; }
        [Parameter] public double? Spacing { get; set; }

        public new XF.StackLayout NativeControl => ((StackLayoutHandler)ElementHandler).StackLayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (Spacing != null)
            {
                builder.AddAttribute(nameof(Spacing), AttributeHelper.DoubleToString(Spacing.Value));
            }
        }
    }
}
