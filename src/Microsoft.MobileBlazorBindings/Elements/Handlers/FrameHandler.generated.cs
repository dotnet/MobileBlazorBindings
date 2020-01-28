// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class FrameHandler : ContentViewHandler
    {
        public FrameHandler(NativeComponentRenderer renderer, XF.Frame frameControl) : base(renderer, frameControl)
        {
            FrameControl = frameControl ?? throw new ArgumentNullException(nameof(frameControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Frame FrameControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Frame.BorderColor):
                    FrameControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.Frame.CornerRadius):
                    FrameControl.CornerRadius = AttributeHelper.StringToSingle((string)attributeValue, -1.00f);
                    break;
                case nameof(XF.Frame.HasShadow):
                    FrameControl.HasShadow = AttributeHelper.GetBool(attributeValue, true);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
