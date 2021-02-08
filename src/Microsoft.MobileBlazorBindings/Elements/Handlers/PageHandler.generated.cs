// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class PageHandler : VisualElementHandler
    {
        private static readonly XF.ImageSource BackgroundImageSourceDefaultValue = XF.Page.BackgroundImageSourceProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly XF.ImageSource IconImageSourceDefaultValue = XF.Page.IconImageSourceProperty.DefaultValue is XF.ImageSource value ? value : default;
        private static readonly bool IsBusyDefaultValue = XF.Page.IsBusyProperty.DefaultValue is bool value ? value : default;
        private static readonly XF.Thickness PaddingDefaultValue = XF.Page.PaddingProperty.DefaultValue is XF.Thickness value ? value : default;
        private static readonly string TitleDefaultValue = XF.Page.TitleProperty.DefaultValue is string value ? value : default;

        public PageHandler(NativeComponentRenderer renderer, XF.Page pageControl) : base(renderer, pageControl)
        {
            PageControl = pageControl ?? throw new ArgumentNullException(nameof(pageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.Page PageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Page.BackgroundImageSource):
                    PageControl.BackgroundImageSource = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, BackgroundImageSourceDefaultValue);
                    break;
                case nameof(XF.Page.IconImageSource):
                    PageControl.IconImageSource = AttributeHelper.DelegateToObject<XF.ImageSource>(attributeValue, IconImageSourceDefaultValue);
                    break;
                case nameof(XF.Page.IsBusy):
                    PageControl.IsBusy = AttributeHelper.GetBool(attributeValue, IsBusyDefaultValue);
                    break;
                case nameof(XF.Page.Padding):
                    PageControl.Padding = AttributeHelper.StringToThickness(attributeValue, PaddingDefaultValue);
                    break;
                case nameof(XF.Page.Title):
                    PageControl.Title = (string)attributeValue ?? TitleDefaultValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
