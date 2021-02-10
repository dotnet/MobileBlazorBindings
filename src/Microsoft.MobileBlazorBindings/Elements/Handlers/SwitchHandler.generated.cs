// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class SwitchHandler : ViewHandler
    {
        private static readonly bool IsToggledDefaultValue = XF.Switch.IsToggledProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.Color OnColorDefaultValue = XF.Switch.OnColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.Color ThumbColorDefaultValue = XF.Switch.ThumbColorProperty.DefaultValue is XF.Color value ? value : default;

        public SwitchHandler(NativeComponentRenderer renderer, XF.Switch switchControl) : base(renderer, switchControl)
        {
            SwitchControl = switchControl ?? throw new ArgumentNullException(nameof(switchControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Switch SwitchControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Switch.IsToggled):
                    SwitchControl.IsToggled = AttributeHelper.GetBool(attributeValue, IsToggledDefaultValue);
                    break;
                case nameof(XF.Switch.OnColor):
                    SwitchControl.OnColor = AttributeHelper.StringToColor((string)attributeValue, OnColorDefaultValue);
                    break;
                case nameof(XF.Switch.ThumbColor):
                    SwitchControl.ThumbColor = AttributeHelper.StringToColor((string)attributeValue, ThumbColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
