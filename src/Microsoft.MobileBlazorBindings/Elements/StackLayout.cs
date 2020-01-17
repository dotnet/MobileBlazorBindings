// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class StackLayout : Layout
    {
        static StackLayout()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<StackLayout>(renderer => new StackLayoutHandler(renderer, new XF.StackLayout()));
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
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

        protected override RenderFragment GetChildContent() => ChildContent;
    }
}
