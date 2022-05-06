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
    public partial class Editor : InputView
    {
        static Editor()
        {
            ElementHandlerRegistry.RegisterElementHandler<Editor>(
                renderer => new EditorHandler(renderer, new MC.Editor()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.EditorAutoSizeOption? AutoSize { get; set; }
        [Parameter] public int? CursorPosition { get; set; }
        [Parameter] public MC.FontAttributes? FontAttributes { get; set; }
        [Parameter] public bool? FontAutoScalingEnabled { get; set; }
        [Parameter] public string FontFamily { get; set; }
        [Parameter] public double? FontSize { get; set; }
        [Parameter] public TextAlignment? HorizontalTextAlignment { get; set; }
        [Parameter] public bool? IsTextPredictionEnabled { get; set; }
        [Parameter] public int? SelectionLength { get; set; }
        [Parameter] public TextAlignment? VerticalTextAlignment { get; set; }

        public new MC.Editor NativeControl => ((EditorHandler)ElementHandler).EditorControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutoSize != null)
            {
                builder.AddAttribute(nameof(AutoSize), (int)AutoSize.Value);
            }
            if (CursorPosition != null)
            {
                builder.AddAttribute(nameof(CursorPosition), CursorPosition.Value);
            }
            if (FontAttributes != null)
            {
                builder.AddAttribute(nameof(FontAttributes), (int)FontAttributes.Value);
            }
            if (FontAutoScalingEnabled != null)
            {
                builder.AddAttribute(nameof(FontAutoScalingEnabled), FontAutoScalingEnabled.Value);
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
            if (IsTextPredictionEnabled != null)
            {
                builder.AddAttribute(nameof(IsTextPredictionEnabled), IsTextPredictionEnabled.Value);
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

        static partial void RegisterAdditionalHandlers();
    }
}
