// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class CheckBoxHandler : ViewHandler
    {
        private static readonly Color ColorDefaultValue = MC.CheckBox.ColorProperty.DefaultValue is Color value ? value : default;
        private static readonly bool IsCheckedDefaultValue = MC.CheckBox.IsCheckedProperty.DefaultValue is bool value ? value : default;

        public CheckBoxHandler(NativeComponentRenderer renderer, MC.CheckBox checkBoxControl) : base(renderer, checkBoxControl)
        {
            CheckBoxControl = checkBoxControl ?? throw new ArgumentNullException(nameof(checkBoxControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.CheckBox CheckBoxControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.CheckBox.Color):
                    CheckBoxControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(MC.CheckBox.IsChecked):
                    CheckBoxControl.IsChecked = AttributeHelper.GetBool(attributeValue, IsCheckedDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
