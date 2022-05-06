// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ImageHandler : ViewHandler
    {
        private static readonly Aspect AspectDefaultValue = MC.Image.AspectProperty.DefaultValue is Aspect value ? value : default;
        private static readonly bool IsAnimationPlayingDefaultValue = MC.Image.IsAnimationPlayingProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsOpaqueDefaultValue = MC.Image.IsOpaqueProperty.DefaultValue is bool value ? value : default;
        private static readonly MC.ImageSource SourceDefaultValue = MC.Image.SourceProperty.DefaultValue is MC.ImageSource value ? value : default;

        public ImageHandler(NativeComponentRenderer renderer, MC.Image imageControl) : base(renderer, imageControl)
        {
            ImageControl = imageControl ?? throw new ArgumentNullException(nameof(imageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Image ImageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Image.Aspect):
                    ImageControl.Aspect = (Aspect)AttributeHelper.GetInt(attributeValue, (int)AspectDefaultValue);
                    break;
                case nameof(MC.Image.IsAnimationPlaying):
                    ImageControl.IsAnimationPlaying = AttributeHelper.GetBool(attributeValue, IsAnimationPlayingDefaultValue);
                    break;
                case nameof(MC.Image.IsOpaque):
                    ImageControl.IsOpaque = AttributeHelper.GetBool(attributeValue, IsOpaqueDefaultValue);
                    break;
                case nameof(MC.Image.Source):
                    ImageControl.Source = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, SourceDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
