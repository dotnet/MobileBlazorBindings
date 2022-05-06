// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class ScrollViewHandler : BlazorBindings.Maui.Elements.Compatibility.Handlers.LayoutHandler
    {
        private static readonly ScrollBarVisibility HorizontalScrollBarVisibilityDefaultValue = MC.ScrollView.HorizontalScrollBarVisibilityProperty.DefaultValue is ScrollBarVisibility value ? value : default;
        private static readonly ScrollOrientation OrientationDefaultValue = MC.ScrollView.OrientationProperty.DefaultValue is ScrollOrientation value ? value : default;
        private static readonly ScrollBarVisibility VerticalScrollBarVisibilityDefaultValue = MC.ScrollView.VerticalScrollBarVisibilityProperty.DefaultValue is ScrollBarVisibility value ? value : default;

        public ScrollViewHandler(NativeComponentRenderer renderer, MC.ScrollView scrollViewControl) : base(renderer, scrollViewControl)
        {
            ScrollViewControl = scrollViewControl ?? throw new ArgumentNullException(nameof(scrollViewControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.ScrollView ScrollViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.ScrollView.HorizontalScrollBarVisibility):
                    ScrollViewControl.HorizontalScrollBarVisibility = (ScrollBarVisibility)AttributeHelper.GetInt(attributeValue, (int)HorizontalScrollBarVisibilityDefaultValue);
                    break;
                case nameof(MC.ScrollView.Orientation):
                    ScrollViewControl.Orientation = (ScrollOrientation)AttributeHelper.GetInt(attributeValue, (int)OrientationDefaultValue);
                    break;
                case nameof(MC.ScrollView.VerticalScrollBarVisibility):
                    ScrollViewControl.VerticalScrollBarVisibility = (ScrollBarVisibility)AttributeHelper.GetInt(attributeValue, (int)VerticalScrollBarVisibilityDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
