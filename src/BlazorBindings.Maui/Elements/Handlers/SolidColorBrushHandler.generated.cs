// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class SolidColorBrushHandler : BrushHandler
    {
        private static readonly Color ColorDefaultValue = MC.SolidColorBrush.ColorProperty.DefaultValue is Color value ? value : default;

        public SolidColorBrushHandler(NativeComponentRenderer renderer, MC.SolidColorBrush solidColorBrushControl) : base(renderer, solidColorBrushControl)
        {
            SolidColorBrushControl = solidColorBrushControl ?? throw new ArgumentNullException(nameof(solidColorBrushControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.SolidColorBrush SolidColorBrushControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.SolidColorBrush.Color):
                    SolidColorBrushControl.Color = AttributeHelper.StringToColor((string)attributeValue, ColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
