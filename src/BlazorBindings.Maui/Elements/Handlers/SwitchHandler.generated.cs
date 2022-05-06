// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class SwitchHandler : ViewHandler
    {
        private static readonly bool IsToggledDefaultValue = MC.Switch.IsToggledProperty.DefaultValue is bool value ? value : default;
        private static readonly Color OnColorDefaultValue = MC.Switch.OnColorProperty.DefaultValue is Color value ? value : default;
        private static readonly Color ThumbColorDefaultValue = MC.Switch.ThumbColorProperty.DefaultValue is Color value ? value : default;

        public SwitchHandler(NativeComponentRenderer renderer, MC.Switch switchControl) : base(renderer, switchControl)
        {
            SwitchControl = switchControl ?? throw new ArgumentNullException(nameof(switchControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Switch SwitchControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Switch.IsToggled):
                    SwitchControl.IsToggled = AttributeHelper.GetBool(attributeValue, IsToggledDefaultValue);
                    break;
                case nameof(MC.Switch.OnColor):
                    SwitchControl.OnColor = AttributeHelper.StringToColor((string)attributeValue, OnColorDefaultValue);
                    break;
                case nameof(MC.Switch.ThumbColor):
                    SwitchControl.ThumbColor = AttributeHelper.StringToColor((string)attributeValue, ThumbColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
