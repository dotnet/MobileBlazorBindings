// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class BoxViewHandler : ViewHandler
    {
        private static readonly Color ColorDefaultValue = MC.BoxView.ColorProperty.DefaultValue is Color value ? value : default;
        private static readonly CornerRadius CornerRadiusDefaultValue = MC.BoxView.CornerRadiusProperty.DefaultValue is CornerRadius value ? value : default;

        public BoxViewHandler(NativeComponentRenderer renderer, MC.BoxView boxViewControl) : base(renderer, boxViewControl)
        {
            BoxViewControl = boxViewControl ?? throw new ArgumentNullException(nameof(boxViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.BoxView BoxViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.BoxView.Color):
                    BoxViewControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                case nameof(MC.BoxView.CornerRadius):
                    BoxViewControl.CornerRadius = AttributeHelper.StringToCornerRadius(attributeValue, CornerRadiusDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
