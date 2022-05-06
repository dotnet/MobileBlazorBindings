// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class FrameHandler : ContentViewHandler
    {
        private static readonly Color BorderColorDefaultValue = MC.Frame.BorderColorProperty.DefaultValue is Color value ? value : default;
        private static readonly float CornerRadiusDefaultValue = MC.Frame.CornerRadiusProperty.DefaultValue is float value ? value : default;
        private static readonly bool HasShadowDefaultValue = MC.Frame.HasShadowProperty.DefaultValue is bool value ? value : default;

        public FrameHandler(NativeComponentRenderer renderer, MC.Frame frameControl) : base(renderer, frameControl)
        {
            FrameControl = frameControl ?? throw new ArgumentNullException(nameof(frameControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Frame FrameControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Frame.BorderColor):
                    FrameControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue, BorderColorDefaultValue);
                    break;
                case nameof(MC.Frame.CornerRadius):
                    FrameControl.CornerRadius = AttributeHelper.StringToSingle((string)attributeValue, CornerRadiusDefaultValue);
                    break;
                case nameof(MC.Frame.HasShadow):
                    FrameControl.HasShadow = AttributeHelper.GetBool(attributeValue, HasShadowDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
