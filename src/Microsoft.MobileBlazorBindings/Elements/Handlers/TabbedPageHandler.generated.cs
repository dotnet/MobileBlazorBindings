// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TabbedPageHandler : PageHandler
    {
        public TabbedPageHandler(NativeComponentRenderer renderer, XF.TabbedPage tabbedPageControl) : base(renderer, tabbedPageControl)
        {
            TabbedPageControl = tabbedPageControl ?? throw new ArgumentNullException(nameof(tabbedPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public XF.TabbedPage TabbedPageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.TabbedPage.BarBackgroundColor):
                    TabbedPageControl.BarBackgroundColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.TabbedPage.BarTextColor):
                    TabbedPageControl.BarTextColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.TabbedPage.SelectedTabColor):
                    TabbedPageControl.SelectedTabColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.TabbedPage.UnselectedTabColor):
                    TabbedPageControl.UnselectedTabColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
