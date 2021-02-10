// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class InputViewHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = XF.InputView.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly bool IsReadOnlyDefaultValue = XF.InputView.IsReadOnlyProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsSpellCheckEnabledDefaultValue = XF.InputView.IsSpellCheckEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.Keyboard KeyboardDefaultValue = XF.InputView.KeyboardProperty.DefaultValue is XF.Keyboard value ? value : default;
        private static readonly int MaxLengthDefaultValue = XF.InputView.MaxLengthProperty.DefaultValue is int value ? value : default;
        private static readonly string PlaceholderDefaultValue = XF.InputView.PlaceholderProperty.DefaultValue is string value ? value : default;
        private static readonly XF.Color PlaceholderColorDefaultValue = XF.InputView.PlaceholderColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly string TextDefaultValue = XF.InputView.TextProperty.DefaultValue is string value ? value : default;
        private static readonly XF.Color TextColorDefaultValue = XF.InputView.TextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.TextTransform TextTransformDefaultValue = XF.InputView.TextTransformProperty.DefaultValue is XF.TextTransform value ? value : default;

        public InputViewHandler(NativeComponentRenderer renderer, XF.InputView inputViewControl) : base(renderer, inputViewControl)
        {
            InputViewControl = inputViewControl ?? throw new ArgumentNullException(nameof(inputViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.InputView InputViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.InputView.CharacterSpacing):
                    InputViewControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(XF.InputView.IsReadOnly):
                    InputViewControl.IsReadOnly = AttributeHelper.GetBool(attributeValue, IsReadOnlyDefaultValue);
                    break;
                case nameof(XF.InputView.IsSpellCheckEnabled):
                    InputViewControl.IsSpellCheckEnabled = AttributeHelper.GetBool(attributeValue, IsSpellCheckEnabledDefaultValue);
                    break;
                case nameof(XF.InputView.Keyboard):
                    InputViewControl.Keyboard = AttributeHelper.DelegateToObject<XF.Keyboard>(attributeValue, KeyboardDefaultValue);
                    break;
                case nameof(XF.InputView.MaxLength):
                    InputViewControl.MaxLength = AttributeHelper.GetInt(attributeValue, MaxLengthDefaultValue);
                    break;
                case nameof(XF.InputView.Placeholder):
                    InputViewControl.Placeholder = (string)attributeValue ?? PlaceholderDefaultValue;
                    break;
                case nameof(XF.InputView.PlaceholderColor):
                    InputViewControl.PlaceholderColor = AttributeHelper.StringToColor((string)attributeValue, PlaceholderColorDefaultValue);
                    break;
                case nameof(XF.InputView.Text):
                    InputViewControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(XF.InputView.TextColor):
                    InputViewControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(XF.InputView.TextTransform):
                    InputViewControl.TextTransform = (XF.TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
