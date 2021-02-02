// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = XF.Label.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.Label.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.Label.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.Label.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly XF.TextAlignment HorizontalTextAlignmentDefaultValue = XF.Label.HorizontalTextAlignmentProperty.DefaultValue is XF.TextAlignment value ? value : default;
        private static readonly XF.LineBreakMode LineBreakModeDefaultValue = XF.Label.LineBreakModeProperty.DefaultValue is XF.LineBreakMode value ? value : default;
        private static readonly double LineHeightDefaultValue = XF.Label.LineHeightProperty.DefaultValue is double value ? value : default;
        private static readonly int MaxLinesDefaultValue = XF.Label.MaxLinesProperty.DefaultValue is int value ? value : default;
        private static readonly XF.Thickness PaddingDefaultValue = XF.Label.PaddingProperty.DefaultValue is XF.Thickness value ? value : default;
        private static readonly string TextDefaultValue = XF.Label.TextProperty.DefaultValue is string value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.Label.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextDecorations TextDecorationsDefaultValue = XF.Label.TextDecorationsProperty.DefaultValue is XF.TextDecorations value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.Label.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;
        private static readonly XF.TextType TextTypeDefaultValue = XF.Label.TextTypeProperty.DefaultValue is XF.TextType value ? value : default;
        private static readonly XF.TextAlignment VerticalTextAlignmentDefaultValue = XF.Label.VerticalTextAlignmentProperty.DefaultValue is XF.TextAlignment value ? value : default;

        public LabelHandler(NativeComponentRenderer renderer, XF.Label labelControl) : base(renderer, labelControl)
        {
            LabelControl = labelControl ?? throw new ArgumentNullException(nameof(labelControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Label LabelControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Label.CharacterSpacing):
                    LabelControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.Label.FontAttributes):
                    LabelControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.Label.FontFamily):
                    LabelControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.Label.FontSize):
                    LabelControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.Label.HorizontalTextAlignment):
                    LabelControl.HorizontalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue, (int)HorizontalTextAlignmentDefaultValue);
                    break;
                case nameof(XF.Label.LineBreakMode):
                    LabelControl.LineBreakMode = (XF.LineBreakMode)AttributeHelper.GetInt(attributeValue, (int)LineBreakModeDefaultValue);
                    break;
                case nameof(XF.Label.LineHeight):
                    LabelControl.LineHeight = AttributeHelper.StringToDouble((string)attributeValue, LineHeightDefaultValue);
                    break;
                case nameof(XF.Label.MaxLines):
                    LabelControl.MaxLines = AttributeHelper.GetInt(attributeValue, MaxLinesDefaultValue);
                    break;
                case nameof(XF.Label.Padding):
                    LabelControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(XF.Label.Text):
                    LabelControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(XF.Label.TextColor):
                    LabelControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.Label.TextDecorations):
                    LabelControl.TextDecorations = (XF.TextDecorations)AttributeHelper.GetInt(attributeValue, (int)TextDecorationsDefaultValue);
                    break;
                case nameof(XF.Label.TextTransform):
                    LabelControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                case nameof(XF.Label.TextType):
                    LabelControl.TextType = (XF.TextType)AttributeHelper.GetInt(attributeValue, (int)TextTypeDefaultValue);
                    break;
                case nameof(XF.Label.VerticalTextAlignment):
                    LabelControl.VerticalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue, (int)VerticalTextAlignmentDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
