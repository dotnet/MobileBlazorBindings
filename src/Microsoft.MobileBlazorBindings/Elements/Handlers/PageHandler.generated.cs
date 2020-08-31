// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class PageHandler : VisualElementHandler
    {
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
                    PageControl.BackgroundImageSource = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                case nameof(XF.Page.IconImageSource):
                    PageControl.IconImageSource = AttributeHelper.DelegateToImageSource(attributeValue);
                    break;
                case nameof(XF.Page.IsBusy):
                    PageControl.IsBusy = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Page.Padding):
                    PageControl.Padding = AttributeHelper.StringToThickness(attributeValue);
                    break;
                case nameof(XF.Page.Title):
                    PageControl.Title = (string)attributeValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
