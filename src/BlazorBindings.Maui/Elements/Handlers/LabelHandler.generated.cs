// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class LabelHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = MC.Label.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.Label.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.Label.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.Label.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.Label.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly TextAlignment HorizontalTextAlignmentDefaultValue = MC.Label.HorizontalTextAlignmentProperty.DefaultValue is TextAlignment value ? value : default;
        private static readonly LineBreakMode LineBreakModeDefaultValue = MC.Label.LineBreakModeProperty.DefaultValue is LineBreakMode value ? value : default;
        private static readonly double LineHeightDefaultValue = MC.Label.LineHeightProperty.DefaultValue is double value ? value : default;
        private static readonly int MaxLinesDefaultValue = MC.Label.MaxLinesProperty.DefaultValue is int value ? value : default;
        private static readonly Thickness PaddingDefaultValue = MC.Label.PaddingProperty.DefaultValue is Thickness value ? value : default;
        private static readonly string TextDefaultValue = MC.Label.TextProperty.DefaultValue is string value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.Label.TextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly TextDecorations TextDecorationsDefaultValue = MC.Label.TextDecorationsProperty.DefaultValue is TextDecorations value ? value : default;
        private static readonly TextTransform TextTransformDefaultValue = MC.Label.TextTransformProperty.DefaultValue is TextTransform value ? value : default;
        private static readonly TextType TextTypeDefaultValue = MC.Label.TextTypeProperty.DefaultValue is TextType value ? value : default;
        private static readonly TextAlignment VerticalTextAlignmentDefaultValue = MC.Label.VerticalTextAlignmentProperty.DefaultValue is TextAlignment value ? value : default;

        public LabelHandler(NativeComponentRenderer renderer, MC.Label labelControl) : base(renderer, labelControl)
        {
            LabelControl = labelControl ?? throw new ArgumentNullException(nameof(labelControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Label LabelControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Label.CharacterSpacing):
                    LabelControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.Label.FontAttributes):
                    LabelControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.Label.FontAutoScalingEnabled):
                    LabelControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.Label.FontFamily):
                    LabelControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.Label.FontSize):
                    LabelControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.Label.HorizontalTextAlignment):
                    LabelControl.HorizontalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue, (int)HorizontalTextAlignmentDefaultValue);
                    break;
                case nameof(MC.Label.LineBreakMode):
                    LabelControl.LineBreakMode = (LineBreakMode)AttributeHelper.GetInt(attributeValue, (int)LineBreakModeDefaultValue);
                    break;
                case nameof(MC.Label.LineHeight):
                    LabelControl.LineHeight = AttributeHelper.StringToDouble((string)attributeValue, LineHeightDefaultValue);
                    break;
                case nameof(MC.Label.MaxLines):
                    LabelControl.MaxLines = AttributeHelper.GetInt(attributeValue, MaxLinesDefaultValue);
                    break;
                case nameof(MC.Label.Padding):
                    LabelControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(MC.Label.Text):
                    LabelControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(MC.Label.TextColor):
                    LabelControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(MC.Label.TextDecorations):
                    LabelControl.TextDecorations = (TextDecorations)AttributeHelper.GetInt(attributeValue, (int)TextDecorationsDefaultValue);
                    break;
                case nameof(MC.Label.TextTransform):
                    LabelControl.TextTransform = (TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                case nameof(MC.Label.TextType):
                    LabelControl.TextType = (TextType)AttributeHelper.GetInt(attributeValue, (int)TextTypeDefaultValue);
                    break;
                case nameof(MC.Label.VerticalTextAlignment):
                    LabelControl.VerticalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue, (int)VerticalTextAlignmentDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
