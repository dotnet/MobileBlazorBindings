// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class EntryHandler : InputViewHandler
    {
        private static readonly ClearButtonVisibility ClearButtonVisibilityDefaultValue = MC.Entry.ClearButtonVisibilityProperty.DefaultValue is ClearButtonVisibility value ? value : default;
        private static readonly int CursorPositionDefaultValue = MC.Entry.CursorPositionProperty.DefaultValue is int value ? value : default;
        private static readonly MC.FontAttributes FontAttributesDefaultValue = MC.Entry.FontAttributesProperty.DefaultValue is MC.FontAttributes value ? value : default;
        private static readonly bool FontAutoScalingEnabledDefaultValue = MC.Entry.FontAutoScalingEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly string FontFamilyDefaultValue = MC.Entry.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = MC.Entry.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly TextAlignment HorizontalTextAlignmentDefaultValue = MC.Entry.HorizontalTextAlignmentProperty.DefaultValue is TextAlignment value ? value : default;
        private static readonly bool IsPasswordDefaultValue = MC.Entry.IsPasswordProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsTextPredictionEnabledDefaultValue = MC.Entry.IsTextPredictionEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly ReturnType ReturnTypeDefaultValue = MC.Entry.ReturnTypeProperty.DefaultValue is ReturnType value ? value : default;
        private static readonly int SelectionLengthDefaultValue = MC.Entry.SelectionLengthProperty.DefaultValue is int value ? value : default;
        private static readonly TextAlignment VerticalTextAlignmentDefaultValue = MC.Entry.VerticalTextAlignmentProperty.DefaultValue is TextAlignment value ? value : default;

        public EntryHandler(NativeComponentRenderer renderer, MC.Entry entryControl) : base(renderer, entryControl)
        {
            EntryControl = entryControl ?? throw new ArgumentNullException(nameof(entryControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Entry EntryControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Entry.ClearButtonVisibility):
                    EntryControl.ClearButtonVisibility = (ClearButtonVisibility)AttributeHelper.GetInt(attributeValue, (int)ClearButtonVisibilityDefaultValue);
                    break;
                case nameof(MC.Entry.CursorPosition):
                    EntryControl.CursorPosition = AttributeHelper.GetInt(attributeValue, CursorPositionDefaultValue);
                    break;
                case nameof(MC.Entry.FontAttributes):
                    EntryControl.FontAttributes = (MC.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(MC.Entry.FontAutoScalingEnabled):
                    EntryControl.FontAutoScalingEnabled = AttributeHelper.GetBool(attributeValue, FontAutoScalingEnabledDefaultValue);
                    break;
                case nameof(MC.Entry.FontFamily):
                    EntryControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(MC.Entry.FontSize):
                    EntryControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(MC.Entry.HorizontalTextAlignment):
                    EntryControl.HorizontalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue, (int)HorizontalTextAlignmentDefaultValue);
                    break;
                case nameof(MC.Entry.IsPassword):
                    EntryControl.IsPassword = AttributeHelper.GetBool(attributeValue, IsPasswordDefaultValue);
                    break;
                case nameof(MC.Entry.IsTextPredictionEnabled):
                    EntryControl.IsTextPredictionEnabled = AttributeHelper.GetBool(attributeValue, IsTextPredictionEnabledDefaultValue);
                    break;
                case nameof(MC.Entry.ReturnType):
                    EntryControl.ReturnType = (ReturnType)AttributeHelper.GetInt(attributeValue, (int)ReturnTypeDefaultValue);
                    break;
                case nameof(MC.Entry.SelectionLength):
                    EntryControl.SelectionLength = AttributeHelper.GetInt(attributeValue, SelectionLengthDefaultValue);
                    break;
                case nameof(MC.Entry.VerticalTextAlignment):
                    EntryControl.VerticalTextAlignment = (TextAlignment)AttributeHelper.GetInt(attributeValue, (int)VerticalTextAlignmentDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
