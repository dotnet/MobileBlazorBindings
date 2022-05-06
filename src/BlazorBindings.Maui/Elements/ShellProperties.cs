// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;

namespace BlazorBindings.Maui.Elements
{
    public class ShellProperties : NativeControlComponentBase
    {
        [Parameter] public bool? NavBarIsVisible { get; set; }
        [Parameter] public bool? NavBarHasShadow { get; set; }
        [Parameter] public bool? TabBarIsVisible { get; set; }
        [Parameter] public Color BackgroundColor { get; set; }
        [Parameter] public Color DisabledColor { get; set; }
        [Parameter] public Color ForegroundColor { get; set; }
        [Parameter] public Color TabBarBackgroundColor { get; set; }
        [Parameter] public Color TabBarDisabledColor { get; set; }
        [Parameter] public Color TabBarForegroundColor { get; set; }
        [Parameter] public Color TabBarTitleColor { get; set; }
        [Parameter] public Color TabBarUnselectedColor { get; set; }
        [Parameter] public Color TitleColor { get; set; }
        [Parameter] public Color UnselectedColor { get; set; }

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
            if (BackgroundColor is not null)
            {
                builder.AddAttribute(nameof(BackgroundColor), AttributeHelper.ColorToString(BackgroundColor));
            }
            if (DisabledColor is not null)
            {
                builder.AddAttribute(nameof(DisabledColor), AttributeHelper.ColorToString(DisabledColor));
            }
            if (ForegroundColor is not null)
            {
                builder.AddAttribute(nameof(ForegroundColor), AttributeHelper.ColorToString(ForegroundColor));
            }
            if (TabBarBackgroundColor is not null)
            {
                builder.AddAttribute(nameof(TabBarBackgroundColor), AttributeHelper.ColorToString(TabBarBackgroundColor));
            }
            if (TabBarDisabledColor is not null)
            {
                builder.AddAttribute(nameof(TabBarDisabledColor), AttributeHelper.ColorToString(TabBarDisabledColor));
            }
            if (TabBarForegroundColor is not null)
            {
                builder.AddAttribute(nameof(TabBarForegroundColor), AttributeHelper.ColorToString(TabBarForegroundColor));
            }
            if (TabBarTitleColor is not null)
            {
                builder.AddAttribute(nameof(TabBarTitleColor), AttributeHelper.ColorToString(TabBarTitleColor));
            }
            if (TabBarUnselectedColor is not null)
            {
                builder.AddAttribute(nameof(TabBarUnselectedColor), AttributeHelper.ColorToString(TabBarUnselectedColor));
            }
            if (TitleColor is not null)
            {
                builder.AddAttribute(nameof(TitleColor), AttributeHelper.ColorToString(TitleColor));
            }
            if (UnselectedColor is not null)
            {
                builder.AddAttribute(nameof(UnselectedColor), AttributeHelper.ColorToString(UnselectedColor));
            }
        }
    }
}
