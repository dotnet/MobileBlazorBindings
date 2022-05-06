// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class InputViewHandler : ViewHandler
    {
        private static readonly double CharacterSpacingDefaultValue = MC.InputView.CharacterSpacingProperty.DefaultValue is double value ? value : default;
        private static readonly bool IsReadOnlyDefaultValue = MC.InputView.IsReadOnlyProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsSpellCheckEnabledDefaultValue = MC.InputView.IsSpellCheckEnabledProperty.DefaultValue is bool value ? value : default;
        private static readonly Keyboard KeyboardDefaultValue = MC.InputView.KeyboardProperty.DefaultValue is Keyboard value ? value : default;
        private static readonly int MaxLengthDefaultValue = MC.InputView.MaxLengthProperty.DefaultValue is int value ? value : default;
        private static readonly string PlaceholderDefaultValue = MC.InputView.PlaceholderProperty.DefaultValue is string value ? value : default;
        private static readonly Color PlaceholderColorDefaultValue = MC.InputView.PlaceholderColorProperty.DefaultValue is Color value ? value : default;
        private static readonly string TextDefaultValue = MC.InputView.TextProperty.DefaultValue is string value ? value : default;
        private static readonly Color TextColorDefaultValue = MC.InputView.TextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly TextTransform TextTransformDefaultValue = MC.InputView.TextTransformProperty.DefaultValue is TextTransform value ? value : default;

        public InputViewHandler(NativeComponentRenderer renderer, MC.InputView inputViewControl) : base(renderer, inputViewControl)
        {
            InputViewControl = inputViewControl ?? throw new ArgumentNullException(nameof(inputViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.InputView InputViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.InputView.CharacterSpacing):
                    InputViewControl.CharacterSpacing = AttributeHelper.StringToDouble((string)attributeValue, CharacterSpacingDefaultValue);
                    break;
                case nameof(MC.InputView.IsReadOnly):
                    InputViewControl.IsReadOnly = AttributeHelper.GetBool(attributeValue, IsReadOnlyDefaultValue);
                    break;
                case nameof(MC.InputView.IsSpellCheckEnabled):
                    InputViewControl.IsSpellCheckEnabled = AttributeHelper.GetBool(attributeValue, IsSpellCheckEnabledDefaultValue);
                    break;
                case nameof(MC.InputView.Keyboard):
                    InputViewControl.Keyboard = AttributeHelper.DelegateToObject<Keyboard>(attributeValue, KeyboardDefaultValue);
                    break;
                case nameof(MC.InputView.MaxLength):
                    InputViewControl.MaxLength = AttributeHelper.GetInt(attributeValue, MaxLengthDefaultValue);
                    break;
                case nameof(MC.InputView.Placeholder):
                    InputViewControl.Placeholder = (string)attributeValue ?? PlaceholderDefaultValue;
                    break;
                case nameof(MC.InputView.PlaceholderColor):
                    InputViewControl.PlaceholderColor = AttributeHelper.StringToColor((string)attributeValue, PlaceholderColorDefaultValue);
                    break;
                case nameof(MC.InputView.Text):
                    InputViewControl.Text = (string)attributeValue ?? TextDefaultValue;
                    break;
                case nameof(MC.InputView.TextColor):
                    InputViewControl.TextColor = AttributeHelper.StringToColor((string)attributeValue, TextColorDefaultValue);
                    break;
                case nameof(MC.InputView.TextTransform):
                    InputViewControl.TextTransform = (TextTransform)AttributeHelper.GetInt(attributeValue, (int)TextTransformDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
