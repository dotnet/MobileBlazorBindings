// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class ImageButton : View
    {
        static ImageButton()
        {
            ElementHandlerRegistry.RegisterElementHandler<ImageButton>(
                renderer => new ImageButtonHandler(renderer, new XF.ImageButton()));
        }

        [Parameter] public XF.Aspect? Aspect { get; set; }
        [Parameter] public XF.Color? BorderColor { get; set; }
        [Parameter] public double? BorderWidth { get; set; }
        [Parameter] public int? CornerRadius { get; set; }
        [Parameter] public bool? IsOpaque { get; set; }
        [Parameter] public XF.Thickness? Padding { get; set; }
        [Parameter] public XF.ImageSource Source { get; set; }

        public new XF.ImageButton NativeControl => ((ImageButtonHandler)ElementHandler).ImageButtonControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Aspect != null)
            {
                builder.AddAttribute(nameof(Aspect), (int)Aspect.Value);
            }
            if (BorderColor != null)
            {
                builder.AddAttribute(nameof(BorderColor), AttributeHelper.ColorToString(BorderColor.Value));
            }
            if (BorderWidth != null)
            {
                builder.AddAttribute(nameof(BorderWidth), AttributeHelper.DoubleToString(BorderWidth.Value));
            }
            if (CornerRadius != null)
            {
                builder.AddAttribute(nameof(CornerRadius), CornerRadius.Value);
            }
            if (IsOpaque != null)
            {
                builder.AddAttribute(nameof(IsOpaque), IsOpaque.Value);
            }
            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }
            if (Source != null)
            {
                builder.AddAttribute(nameof(Source), AttributeHelper.ObjectToDelegate(Source));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
