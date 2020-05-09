// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Entry : InputView
    {
        static Entry()
        {
            ElementHandlerRegistry.RegisterElementHandler<Entry>(
                renderer => new EntryHandler(renderer, new XF.Entry()));
        }

        [Parameter] public XF.ClearButtonVisibility? ClearButtonVisibility { get; set; }
        [Parameter] public int? CursorPosition { get; set; }
        [Parameter] public XF.FontAttributes? FontAttributes { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public XF.TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public bool? IsPassword { get; set; }
        [Parameter] public bool? IsTextPredictionEnabled { get; set; }
        [Parameter] public XF.ReturnType? ReturnType { get; set; }
        [Parameter] public int? SelectionLength { get; set; }
        [Parameter] public XF.TextAlignment? VerticalTextAlignment { get; set; }

        public new XF.Entry NativeControl => ((EntryHandler)ElementHandler).EntryControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (ClearButtonVisibility != null)
            {
                builder.AddAttribute(nameof(ClearButtonVisibility), (int)ClearButtonVisibility.Value);
            }
            if (CursorPosition != null)
            {
                builder.AddAttribute(nameof(CursorPosition), CursorPosition.Value);
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (FontFamily != null)
            {
                builder.AddAttribute(nameof(FontFamily), FontFamily);
            }
            if (FontSize != null)
            {
                builder.AddAttribute(nameof(FontSize), AttributeHelper.DoubleToString(FontSize.Value));
            }
            if (HorizontalTextAlignment != null)
            {
                builder.AddAttribute(nameof(HorizontalTextAlignment), (int)HorizontalTextAlignment.Value);
            }
            if (IsPassword != null)
            {
                builder.AddAttribute(nameof(IsPassword), IsPassword.Value);
            }
            if (IsTextPredictionEnabled != null)
            {
                builder.AddAttribute(nameof(IsTextPredictionEnabled), IsTextPredictionEnabled.Value);
            }
            if (ReturnType != null)
            {
                builder.AddAttribute(nameof(ReturnType), (int)ReturnType.Value);
            }
            if (SelectionLength != null)
            {
                builder.AddAttribute(nameof(SelectionLength), SelectionLength.Value);
            }
            if (VerticalTextAlignment != null)
            {
                builder.AddAttribute(nameof(VerticalTextAlignment), (int)VerticalTextAlignment.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
