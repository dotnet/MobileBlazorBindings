// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class CheckBoxHandler : ViewHandler
    {
        private static readonly XF.Color ColorDefaultValue = XF.CheckBox.ColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly bool IsCheckedDefaultValue = XF.CheckBox.IsCheckedProperty.DefaultValue is bool value ? value : default;

        public CheckBoxHandler(NativeComponentRenderer renderer, XF.CheckBox checkBoxControl) : base(renderer, checkBoxControl)
        {
            CheckBoxControl = checkBoxControl ?? throw new ArgumentNullException(nameof(checkBoxControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.CheckBox CheckBoxControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.CheckBox.Color):
                    CheckBoxControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(XF.CheckBox.IsChecked):
                    CheckBoxControl.IsChecked = AttributeHelper.GetBool(attributeValue, IsCheckedDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
