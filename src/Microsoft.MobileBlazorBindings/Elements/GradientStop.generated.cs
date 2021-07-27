// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class GradientStop : Element
    {
        static GradientStop()
        {
            ElementHandlerRegistry.RegisterElementHandler<GradientStop>(
                renderer => new GradientStopHandler(renderer, new XF.GradientStop()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public XF.Color? Color { get; set; }
        [Parameter] public float? Offset { get; set; }

        public new XF.GradientStop NativeControl => ((GradientStopHandler)ElementHandler).GradientStopControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color.Value));
            }
            if (Offset != null)
            {
                builder.AddAttribute(nameof(Offset), AttributeHelper.SingleToString(Offset.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
