// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ImageHandler : ViewHandler
    {
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
                    ImageControl.Aspect = (XF.Aspect)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Image.IsAnimationPlaying):
                    ImageControl.IsAnimationPlaying = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Image.IsOpaque):
                    ImageControl.IsOpaque = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Image.Source):
                    ImageControl.Source = AttributeHelper.StringToImageSource(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
