// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class EntryHandler : InputViewHandler
    {
        private static readonly XF.ClearButtonVisibility ClearButtonVisibilityDefaultValue = XF.Entry.ClearButtonVisibilityProperty.DefaultValue is XF.ClearButtonVisibility value ? value : default;
        private static readonly int CursorPositionDefaultValue = XF.Entry.CursorPositionProperty.DefaultValue is int value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.Entry.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.Entry.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.Entry.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly XF.TextAlignment HorizontalTextAlignmentDefaultValue = XF.Entry.HorizontalTextAlignmentProperty.DefaultValue is XF.TextAlignment value ? value : default;
        private static readonly bool IsPasswordDefaultValue = XF.Entry.IsPasswordProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsTextPredictionEnabledDefaultValue = XF.Entry.IsTextPredictionEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.ReturnType ReturnTypeDefaultValue = XF.Entry.ReturnTypeProperty.DefaultValue is XF.ReturnType value ? value : default;
        private static readonly int SelectionLengthDefaultValue = XF.Entry.SelectionLengthProperty.DefaultValue is int value ? value : default;
        private static readonly XF.TextAlignment VerticalTextAlignmentDefaultValue = XF.Entry.VerticalTextAlignmentProperty.DefaultValue is XF.TextAlignment value ? value : default;

        public EntryHandler(NativeComponentRenderer renderer, XF.Entry entryControl) : base(renderer, entryControl)
        {
            EntryControl = entryControl ?? throw new ArgumentNullException(nameof(entryControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Entry EntryControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Entry.ClearButtonVisibility):
                    EntryControl.ClearButtonVisibility = (XF.ClearButtonVisibility)AttributeHelper.GetInt(attributeValue, (int)ClearButtonVisibilityDefaultValue);
                    break;
                case nameof(XF.Entry.CursorPosition):
                    EntryControl.CursorPosition = AttributeHelper.GetInt(attributeValue, CursorPositionDefaultValue);
                    break;
                case nameof(XF.Entry.FontAttributes):
                    EntryControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.Entry.FontFamily):
                    EntryControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.Entry.FontSize):
                    EntryControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.Entry.HorizontalTextAlignment):
                    EntryControl.HorizontalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue, (int)HorizontalTextAlignmentDefaultValue);
                    break;
                case nameof(XF.Entry.IsPassword):
                    EntryControl.IsPassword = AttributeHelper.GetBool(attributeValue, IsPasswordDefaultValue);
                    break;
                case nameof(XF.Entry.IsTextPredictionEnabled):
                    EntryControl.IsTextPredictionEnabled = AttributeHelper.GetBool(attributeValue, IsTextPredictionEnabledDefaultValue);
                    break;
                case nameof(XF.Entry.ReturnType):
                    EntryControl.ReturnType = (XF.ReturnType)AttributeHelper.GetInt(attributeValue, (int)ReturnTypeDefaultValue);
                    break;
                case nameof(XF.Entry.SelectionLength):
                    EntryControl.SelectionLength = AttributeHelper.GetInt(attributeValue, SelectionLengthDefaultValue);
                    break;
                case nameof(XF.Entry.VerticalTextAlignment):
                    EntryControl.VerticalTextAlignment = (XF.TextAlignment)AttributeHelper.GetInt(attributeValue, (int)VerticalTextAlignmentDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
