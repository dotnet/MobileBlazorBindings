// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using BlazorBindings.Core;
using System;
using System.Threading.Tasks;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements
{
    public partial class Shell : Page
    {
        static partial void RegisterAdditionalHandlers()
        {
            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.NavBarIsVisible",
                (element, value) => MC.Shell.SetNavBarIsVisible(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.NavBarHasShadow",
                (element, value) => MC.Shell.SetNavBarHasShadow(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarIsVisible",
                (element, value) => MC.Shell.SetTabBarIsVisible(element, AttributeHelper.GetBool(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.BackgroundColor",
                (element, value) => MC.Shell.SetBackgroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.DisabledColor",
                (element, value) => MC.Shell.SetDisabledColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.ForegroundColor",
                (element, value) => MC.Shell.SetForegroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarBackgroundColor",
                (element, value) => MC.Shell.SetTabBarBackgroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarDisabledColor",
                (element, value) => MC.Shell.SetTabBarDisabledColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarForegroundColor",
                (element, value) => MC.Shell.SetTabBarForegroundColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarTitleColor",
                (element, value) => MC.Shell.SetTabBarTitleColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TabBarUnselectedColor",
                (element, value) => MC.Shell.SetTabBarUnselectedColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.TitleColor",
                (element, value) => MC.Shell.SetTitleColor(element, AttributeHelper.StringToColor(value)));

            AttachedPropertyRegistry.RegisterAttachedPropertyHandler("Shell.UnselectedColor",
                (element, value) => MC.Shell.SetUnselectedColor(element, AttributeHelper.StringToColor(value)));
        }

        [Parameter] public RenderFragment FlyoutHeader { get; set; }

        [Parameter] public EventCallback<MC.ShellNavigatedEventArgs> OnNavigated { get; set; }
        [Parameter] public EventCallback<MC.ShellNavigatingEventArgs> OnNavigating { get; set; }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            builder.AddAttribute("onnavigated", OnNavigated);
            builder.AddAttribute("onnavigating", OnNavigating);
        }

        public async Task GoTo(MC.ShellNavigationState state, bool animate = true)
        {
            if (state is null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            await NativeControl.GoToAsync(state, animate).ConfigureAwait(true);
        }

        protected override RenderFragment GetChildContent() => RenderChildContent;

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            if (FlyoutHeader != null)
            {
                builder.OpenComponent<ShellFlyoutHeader>(1);
                builder.AddAttribute(0, nameof(ChildContent), FlyoutHeader);
                builder.CloseComponent();
            }

            builder.AddContent(2, ChildContent);
        }
    }
}
