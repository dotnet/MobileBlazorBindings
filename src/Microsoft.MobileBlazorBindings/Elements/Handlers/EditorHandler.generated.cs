// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class EditorHandler : InputViewHandler
    {
        private static readonly XF.EditorAutoSizeOption AutoSizeDefaultValue = XF.Editor.AutoSizeProperty.DefaultValue is XF.EditorAutoSizeOption value ? value : default;
        private static readonly XF.FontAttributes FontAttributesDefaultValue = XF.Editor.FontAttributesProperty.DefaultValue is XF.FontAttributes value ? value : default;
        private static readonly string FontFamilyDefaultValue = XF.Editor.FontFamilyProperty.DefaultValue is string value ? value : default;
        private static readonly double FontSizeDefaultValue = XF.Editor.FontSizeProperty.DefaultValue is double value ? value : default;
        private static readonly bool IsTextPredictionEnabledDefaultValue = XF.Editor.IsTextPredictionEnabledProperty.DefaultValue is bool value ? value : default;

        public EditorHandler(NativeComponentRenderer renderer, XF.Editor editorControl) : base(renderer, editorControl)
        {
            EditorControl = editorControl ?? throw new ArgumentNullException(nameof(editorControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Editor EditorControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Editor.AutoSize):
                    EditorControl.AutoSize = (XF.EditorAutoSizeOption)AttributeHelper.GetInt(attributeValue, (int)AutoSizeDefaultValue);
                    break;
                case nameof(XF.Editor.FontAttributes):
                    EditorControl.FontAttributes = (XF.FontAttributes)AttributeHelper.GetInt(attributeValue, (int)FontAttributesDefaultValue);
                    break;
                case nameof(XF.Editor.FontFamily):
                    EditorControl.FontFamily = (string)attributeValue ?? FontFamilyDefaultValue;
                    break;
                case nameof(XF.Editor.FontSize):
                    EditorControl.FontSize = AttributeHelper.StringToDouble((string)attributeValue, FontSizeDefaultValue);
                    break;
                case nameof(XF.Editor.IsTextPredictionEnabled):
                    EditorControl.IsTextPredictionEnabled = AttributeHelper.GetBool(attributeValue, IsTextPredictionEnabledDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
