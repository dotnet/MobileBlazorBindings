// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SpanHandler : GestureElementHandler
    {
        private static readonly XF.Color BackgroundColorDefaultValue = XF.Span.BackgroundColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly double CharacterSpacingDefaultValue = XF.Span.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.Span.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.Span.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.Span.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly XF.Color ForegroundColorDefaultValue = XF.Span.ForegroundColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly double LineHeightDefaultValue = XF.Span.LineHeightProperty.DefaultValue is double value ? value : default;
        private static readonly string TextDefaultValue = XF.Span.TextProperty.DefaultValue is string value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.Span.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextDecorations TextDecorationsDefaultValue = XF.Span.TextDecorationsProperty.DefaultValue is XF.TextDecorations value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.Span.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;

        public SpanHandler(NativeComponentRenderer renderer, XF.Span spanControl) : base(renderer, spanControl)
        {
            SpanControl = spanControl ?? throw new ArgumentNullException(nameof(spanControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Span SpanControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Span.BackgroundColor):
                    SpanControl.BackgroundColor = AttributeHelper.StringToColor((string)attributeValue, BackgroundColorDefaultValue);
                    break;
                case nameof(XF.Span.CharacterSpacing):
                    SpanControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.Span.FontAttributes):
                    SpanControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.Span.FontFamily):
                    SpanControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.Span.FontSize):
                    SpanControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.Span.ForegroundColor):
                    SpanControl.ForegroundColor = AttributeHelper.StringToColor((string)attributeValue, ForegroundColorDefaultValue);
                    break;
                case nameof(XF.Span.LineHeight):
                    SpanControl.LineHeight = AttributeHelper.StringToDouble((string)attributeValue, LineHeightDefaultValue);
                    break;
                case nameof(XF.Span.Text):
                    SpanControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(XF.Span.TextColor):
                    SpanControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.Span.TextDecorations):
                    SpanControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue, (int)TextDecorationsDefaultValue);
                    break;
                case nameof(XF.Span.TextTransform):
                    SpanControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
