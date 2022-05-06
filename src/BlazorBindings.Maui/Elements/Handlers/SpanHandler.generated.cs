// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class SpanHandler : GestureElementHandler
    {
        private static readonly Color BackgroundColorDefaultValue = MC.Span.BackgroundColorProperty.DefaultValue is Color value ? value : default;
        private static readonly double CharacterSpacingDefaultValue = MC.Span.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.Span.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.Span.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.Span.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.Span.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly double LineHeightDefaultValue = MC.Span.LineHeightProperty.DefaultValue is double value ? value : default;
        private static readonly string TextDefaultValue = MC.Span.TextProperty.DefaultValue is string value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.Span.TextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly TextDecorations TextDecorationsDefaultValue = MC.Span.TextDecorationsProperty.DefaultValue is TextDecorations value ? value : default;
        private static readonly TextTransform TextTransformDefaultValue = MC.Span.TextTransformProperty.DefaultValue is TextTransform value ? value : default;

        public SpanHandler(NativeComponentRenderer renderer, MC.Span spanControl) : base(renderer, spanControl)
        {
            SpanControl = spanControl ?? throw new ArgumentNullException(nameof(spanControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Span SpanControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Span.BackgroundColor):
                    SpanControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue, BackgroundColorDefaultValue);
                    break;
                case nameof(MC.Span.CharacterSpacing):
                    SpanControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.Span.FontAttributes):
                    SpanControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.Span.FontAutoScalingEnabled):
                    SpanControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.Span.FontFamily):
                    SpanControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.Span.FontSize):
                    SpanControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.Span.LineHeight):
                    SpanControl.LineHeight = AttributeHelper.StringToDouble((string)attributeValue, LineHeightDefaultValue);
                    break;
                case nameof(MC.Span.Text):
                    SpanControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(MC.Span.TextColor):
                    SpanControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(MC.Span.TextDecorations):
                    SpanControl.TextDecorations = (TextDecorations)AttributeHelper.GetInt(attributeValue, (int)TextDecorationsDefaultValue);
                    break;
                case nameof(MC.Span.TextTransform):
                    SpanControl.TextTransform = (TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
