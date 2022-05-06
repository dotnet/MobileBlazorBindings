// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class PageHandler : VisualElementHandler
    {
        private static readonly MC.ImageSource BackgroundImageSourceDefaultValue = MC.Page.BackgroundImageSourceProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly MC.ImageSource IconImageSourceDefaultValue = MC.Page.IconImageSourceProperty.DefaultValue is MC.ImageSource value ? value : default;
        private static readonly bool IsBusyDefaultValue = MC.Page.IsBusyProperty.DefaultValue is bool value ? value : default;
        private static readonly Thickness PaddingDefaultValue = MC.Page.PaddingProperty.DefaultValue is Thickness value ? value : default;
        private static readonly string TitleDefaultValue = MC.Page.TitleProperty.DefaultValue is string value ? value : default;

        public PageHandler(NativeComponentRenderer renderer, MC.Page pageControl) : base(renderer, pageControl)
        {
            PageControl = pageControl ?? throw new ArgumentNullException(nameof(pageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.Page PageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.Page.BackgroundImageSource):
                    PageControl.BackgroundImageSource = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, BackgroundImageSourceDefaultValue);
                    break;
                case nameof(MC.Page.IconImageSource):
                    PageControl.IconImageSource = AttributeHelper.DelegateToObject<MC.ImageSource>(attributeValue, IconImageSourceDefaultValue);
                    break;
                case nameof(MC.Page.IsBusy):
                    PageControl.IsBusy = AttributeHelper.GetBool(attributeValue, IsBusyDefaultValue);
                    break;
                case nameof(MC.Page.Padding):
                    PageControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(MC.Page.Title):
                    PageControl.Title = (string)attributeValue ?? TitleDefaultValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
