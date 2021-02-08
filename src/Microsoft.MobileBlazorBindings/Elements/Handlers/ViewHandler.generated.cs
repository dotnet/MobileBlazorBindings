// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class ViewHandler : VisualElementHandler
    {
        private static readonly XF.LayoutOptions HorizontalOptionsDefaultValue = XF.View.HorizontalOptionsProperty.DefaultValue is XF.LayoutOptions value ? value : default;
        private static readonly XF.Thickness MarginDefaultValue = XF.View.MarginProperty.DefaultValue is XF.Thickness value ? value : default;
        private static readonly XF.LayoutOptions VerticalOptionsDefaultValue = XF.View.VerticalOptionsProperty.DefaultValue is XF.LayoutOptions value ? value : default;

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
                    ViewControl.HorizontalOptions = AttributeHelper.StringToLayoutOptions(attributeValue, HorizontalOptionsDefaultValue);
                    break;
                case nameof(XF.View.Margin):
                    ViewControl.Margin = AttributeHelper.StringToThickness(attributeValue, MarginDefaultValue);
                    break;
                case nameof(XF.View.VerticalOptions):
                    ViewControl.VerticalOptions = AttributeHelper.StringToLayoutOptions(attributeValue, VerticalOptionsDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
