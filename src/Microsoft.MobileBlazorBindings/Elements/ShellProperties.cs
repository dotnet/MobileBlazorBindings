// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class ShellProperties : NativeControlComponentBase
    {
        [Parameter] public bool? NavBarIsVisible { get; set; }
        [Parameter] public bool? NavBarHasShadow { get; set; }
        [Parameter] public bool? TabBarIsVisible { get; set; }
        [Parameter] public XF.Color? BackgroundColor { get; set; }
        [Parameter] public XF.Color? DisabledColor { get; set; }
        [Parameter] public XF.Color? ForegroundColor { get; set; }
        [Parameter] public XF.Color? TabBarBackgroundColor { get; set; }
        [Parameter] public XF.Color? TabBarDisabledColor { get; set; }
        [Parameter] public XF.Color? TabBarForegroundColor { get; set; }
        [Parameter] public XF.Color? TabBarTitleColor { get; set; }
        [Parameter] public XF.Color? TabBarUnselectedColor { get; set; }
        [Parameter] public XF.Color? TitleColor { get; set; }
        [Parameter] public XF.Color? UnselectedColor { get; set; }

        static ShellProperties()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<ShellProperties>(_ => new ShellPropertiesHandler());
        }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (NavBarIsVisible.HasValue)
            {
                builder.AddAttribute(nameof(NavBarIsVisible), NavBarIsVisible.Value);
            }
            if (NavBarHasShadow.HasValue)
            {
                builder.AddAttribute(nameof(NavBarHasShadow), NavBarHasShadow.Value);
            }
            if (TabBarIsVisible.HasValue)
            {
                builder.AddAttribute(nameof(TabBarIsVisible), TabBarIsVisible.Value);
            }
            if (BackgroundColor.HasValue)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor.Value));
            }
            if (DisabledColor.HasValue)
            {
                builder.AddAttribute(nameof(DisabledColor), AttributeHelper.ColorToString(DisabledColor.Value));
            }
            if (ForegroundColor.HasValue)
            {
                builder.AddAttribute(nameof(ForegroundColor), AttributeHelper.ColorToString(ForegroundColor.Value));
            }
            if (TabBarBackgroundColor.HasValue)
            {
                builder.AddAttribute(nameof(TabBarBackgroundColor), AttributeHelper.ColorToString(TabBarBackgroundColor.Value));
            }
            if (TabBarDisabledColor.HasValue)
            {
                builder.AddAttribute(nameof(TabBarDisabledColor), AttributeHelper.ColorToString(TabBarDisabledColor.Value));
            }
            if (TabBarForegroundColor.HasValue)
            {
                builder.AddAttribute(nameof(TabBarForegroundColor), AttributeHelper.ColorToString(TabBarForegroundColor.Value));
            }
            if (TabBarTitleColor.HasValue)
            {
                builder.AddAttribute(nameof(TabBarTitleColor), AttributeHelper.ColorToString(TabBarTitleColor.Value));
            }
            if (TabBarUnselectedColor.HasValue)
            {
                builder.AddAttribute(nameof(TabBarUnselectedColor), AttributeHelper.ColorToString(TabBarUnselectedColor.Value));
            }
            if (TitleColor.HasValue)
            {
                builder.AddAttribute(nameof(TitleColor), AttributeHelper.ColorToString(TitleColor.Value));
            }
            if (UnselectedColor.HasValue)
            {
                builder.AddAttribute(nameof(UnselectedColor), AttributeHelper.ColorToString(UnselectedColor.Value));
            }
        }
    }
}
