// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public partial class TabbedPageHandler : PageHandler
    {
        private static readonly XF.Color BarBackgroundColorDefaultValue = XF.TabbedPage.BarBackgroundColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.Color BarTextColorDefaultValue = XF.TabbedPage.BarTextColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.Color SelectedTabColorDefaultValue = XF.TabbedPage.SelectedTabColorProperty.DefaultValue is XF.Color value ? value : default;
        private static readonly XF.Color UnselectedTabColorDefaultValue = XF.TabbedPage.UnselectedTabColorProperty.DefaultValue is XF.Color value ? value : default;

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
                    TabbedPageControl.BarBackgroundColor = AttributeHelper.StringToColor((string)attributeValue, BarBackgroundColorDefaultValue);
                    break;
                case nameof(XF.TabbedPage.BarTextColor):
                    TabbedPageControl.BarTextColor = AttributeHelper.StringToColor((string)attributeValue, BarTextColorDefaultValue);
                    break;
                case nameof(XF.TabbedPage.SelectedTabColor):
                    TabbedPageControl.SelectedTabColor = AttributeHelper.StringToColor((string)attributeValue, SelectedTabColorDefaultValue);
                    break;
                case nameof(XF.TabbedPage.UnselectedTabColor):
                    TabbedPageControl.UnselectedTabColor = AttributeHelper.StringToColor((string)attributeValue, UnselectedTabColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
