// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ImageHandler : ViewHandler
    {
        private static readonly XF.Aspect AspectDefaultValue = XF.Image.AspectProperty.DefaultValue is XF.Aspect value ? value : default;
        private static readonly bool IsAnimationPlayingDefaultValue = XF.Image.IsAnimationPlayingProperty.DefaultValue is bool value ? value : default;
        private static readonly bool IsOpaqueDefaultValue = XF.Image.IsOpaqueProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.ImageSource SourceDefaultValue = XF.Image.SourceProperty.DefaultValue is XF.ImageSource value ? value : default;

        public ImageHandler(NativeComponentRenderer renderer, XF.Image imageControl) : base(renderer, imageControl)
        {
            ImageControl = imageControl ?? throw new ArgumentNullException(nameof(imageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Image ImageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Image.Aspect):
                    ImageControl.Aspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue, (int)AspectDefaultValue);
                    break;
                case nameof(XF.Image.IsAnimationPlaying):
                    ImageControl.IsAnimationPlaying = AttributeHelper.GetBool(attributeValue, IsAnimationPlayingDefaultValue);
                    break;
                case nameof(XF.Image.IsOpaque):
                    ImageControl.IsOpaque = AttributeHelper.GetBool(attributeValue, IsOpaqueDefaultValue);
                    break;
                case nameof(XF.Image.Source):
                    ImageControl.Source = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, SourceDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
