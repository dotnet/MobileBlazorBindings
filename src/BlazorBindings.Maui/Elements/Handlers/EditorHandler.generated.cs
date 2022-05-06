// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class EditorHandler : InputViewHandler
    {
        private static readonly MC.EditorAutoSizeOption AutoSizeDefaultValue = MC.Editor.AutoSizeProperty.DefaultValue is MC.EditorAutoSizeOption value ? value : default;
        private static readonly int CursorPositionDefaultValue = MC.Editor.CursorPositionProperty.DefaultValue is int value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.Editor.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.Editor.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.Editor.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.Editor.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly TextAlignment HorizontalTextAlignmentDefaultValue = MC.Editor.HorizontalTextAlignmentProperty.DefaultValue is TextAlignment value ? value : default;
        private static readonly bool IsTextPredictionEnabledDefaultValue = MC.Editor.IsTextPredictionEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly int SelectionLengthDefaultValue = MC.Editor.SelectionLengthProperty.DefaultValue is int value ? value : default;
        private static readonly TextAlignment VerticalTextAlignmentDefaultValue = MC.Editor.VerticalTextAlignmentProperty.DefaultValue is TextAlignment value ? value : default;

        public EditorHandler(NativeComponentRenderer renderer, MC.Editor editorControl) : base(renderer, editorControl)
        {
            EditorControl = editorControl ?? throw new ArgumentNullException(nameof(editorControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Editor EditorControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Editor.AutoSize):
                    EditorControl.AutoSize = (MC.EditorAutoSizeOption)AttributeHelper.GetInt(attributeValue, (int)AutoSizeDefaultValue);
                    break;
                case nameof(MC.Editor.CursorPosition):
                    EditorControl.CursorPosition = AttributeHelper.GetInt(attributeValue, CursorPositionDefaultValue);
                    break;
                case nameof(MC.Editor.FontAttributes):
                    EditorControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.Editor.FontAutoScalingEnabled):
                    EditorControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.Editor.FontFamily):
                    EditorControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.Editor.FontSize):
                    EditorControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.Editor.HorizontalTextAlignment):
                    EditorControl.HorizontalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue, (int)HorizontalTextAlignmentDefaultValue);
                    break;
                case nameof(MC.Editor.IsTextPredictionEnabled):
                    EditorControl.IsTextPredictionEnabled = AttributeHelper.GetBool(attributeValue, IsTextPredictionEnabledDefaultValue);
                    break;
                case nameof(MC.Editor.SelectionLength):
                    EditorControl.SelectionLength = AttributeHelper.GetInt(attributeValue, SelectionLengthDefaultValue);
                    break;
                case nameof(MC.Editor.VerticalTextAlignment):
                    EditorControl.VerticalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue, (int)VerticalTextAlignmentDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
