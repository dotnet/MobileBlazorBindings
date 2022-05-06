// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ViewHandler : VisualElementHandler
    {
        private static readonly MC.LayoutOptions HorizontalOptionsDefaultValue = MC.View.HorizontalOptionsProperty.DefaultValue is MC.LayoutOptions value ? value : default;
        private static readonly Thickness MarginDefaultValue = MC.View.MarginProperty.DefaultValue is Thickness value ? value : default;
        private static readonly MC.LayoutOptions VerticalOptionsDefaultValue = MC.View.VerticalOptionsProperty.DefaultValue is MC.LayoutOptions value ? value : default;

        public ViewHandler(NativeComponentRenderer renderer, MC.View viewControl) : base(renderer, viewControl)
        {
            ViewControl = viewControl ?? throw new ArgumentNullException(nameof(viewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.View ViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.View.HorizontalOptions):
                    ViewControl.HorizontalOptions = AttributeHelper.StringToLayoutOptions(attributeValue, HorizontalOptionsDefaultValue);
                    break;
                case nameof(MC.View.Margin):
                    ViewControl.Margin = AttributeHelper.StringToThickness(attributeValue, MarginDefaultValue);
                    break;
                case nameof(MC.View.VerticalOptions):
                    ViewControl.VerticalOptions = AttributeHelper.StringToLayoutOptions(attributeValue, VerticalOptionsDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
