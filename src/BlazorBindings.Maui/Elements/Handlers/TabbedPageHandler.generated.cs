// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using System;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public partial class TabbedPageHandler : PageHandler
    {
        private static readonly Color BarBackgroundColorDefaultValue = MC.TabbedPage.BarBackgroundColorProperty.DefaultValue is Color value ? value : default;
        private static readonly Color BarTextColorDefaultValue = MC.TabbedPage.BarTextColorProperty.DefaultValue is Color value ? value : default;
        private static readonly Color SelectedTabColorDefaultValue = MC.TabbedPage.SelectedTabColorProperty.DefaultValue is Color value ? value : default;
        private static readonly Color UnselectedTabColorDefaultValue = MC.TabbedPage.UnselectedTabColorProperty.DefaultValue is Color value ? value : default;

        public TabbedPageHandler(NativeComponentRenderer renderer, MC.TabbedPage tabbedPageControl) : base(renderer, tabbedPageControl)
        {
            TabbedPageControl = tabbedPageControl ?? throw new ArgumentNullException(nameof(tabbedPageControl));

            Initialize(renderer);
        }

        partial void Initialize(NativeComponentRenderer renderer);

        public MC.TabbedPage TabbedPageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(MC.TabbedPage.BarBackgroundColor):
                    TabbedPageControl.BarBackgroundColor = AttributeHelper.StringToColor((string)attributeValue, BarBackgroundColorDefaultValue);
                    break;
                case nameof(MC.TabbedPage.BarTextColor):
                    TabbedPageControl.BarTextColor = AttributeHelper.StringToColor((string)attributeValue, BarTextColorDefaultValue);
                    break;
                case nameof(MC.TabbedPage.SelectedTabColor):
                    TabbedPageControl.SelectedTabColor = AttributeHelper.StringToColor((string)attributeValue, SelectedTabColorDefaultValue);
                    break;
                case nameof(MC.TabbedPage.UnselectedTabColor):
                    TabbedPageControl.UnselectedTabColor = AttributeHelper.StringToColor((string)attributeValue, UnselectedTabColorDefaultValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
