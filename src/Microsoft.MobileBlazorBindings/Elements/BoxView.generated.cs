// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class BoxView : View
    {
        static BoxView()
        {
            ElementHandlerRegistry.RegisterElementHandler<BoxView>(
                renderer => new BoxViewHandler(renderer, new XF.BoxView()));
        }

        [Parameter] public XF.Color? Color { get; set; }
        [Parameter] public XF.CornerRadius? CornerRadius { get; set; }

        public new XF.BoxView NativeControl => ((BoxViewHandler)ElementHandler).BoxViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), AttributeHelper.CornerRadiusToString(CornerRadius.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
