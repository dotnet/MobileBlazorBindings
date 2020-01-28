// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ViewHandler : VisualElementHandler
    {
        public ViewHandler(NativeComponentRenderer renderer, XF.View viewControl) : base(renderer, viewControl)
        {
            ViewControl = viewControl ?? throw new ArgumentNullException(nameof(viewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.View ViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.View.HorizontalOptions):
                    ViewControl.HorizontalOptions = AttributeHelper.StringToLayoutOptions(attributeValue, XF.LayoutOptions.Fill);
                    break;
                case nameof(XF.View.Margin):
                    ViewControl.Margin = AttributeHelper.StringToThickness(attributeValue);
                    break;
                case nameof(XF.View.VerticalOptions):
                    ViewControl.VerticalOptions = AttributeHelper.StringToLayoutOptions(attributeValue, XF.LayoutOptions.Fill);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
