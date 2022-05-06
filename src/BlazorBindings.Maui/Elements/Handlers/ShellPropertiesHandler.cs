// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.Maui.Graphics;
using MC = Microsoft.Maui.Controls;

namespace BlazorBindings.Maui.Elements.Handlers
{
    public class ShellPropertiesHandler : BaseAttachedPropertiesHandler
    {
        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(ShellProperties.NavBarIsVisible):
                    MC.Shell.SetNavBarIsVisible(Target, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.NavBarHasShadow):
                    MC.Shell.SetNavBarHasShadow(Target, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarIsVisible):
                    MC.Shell.SetTabBarIsVisible(Target, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.BackgroundColor):
                    MC.Shell.SetBackgroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.DisabledColor):
                    MC.Shell.SetDisabledColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.ForegroundColor):
                    MC.Shell.SetForegroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarBackgroundColor):
                    MC.Shell.SetTabBarBackgroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarDisabledColor):
                    MC.Shell.SetTabBarDisabledColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarForegroundColor):
                    MC.Shell.SetTabBarForegroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarTitleColor):
                    MC.Shell.SetTabBarTitleColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarUnselectedColor):
                    MC.Shell.SetTabBarUnselectedColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TitleColor):
                    MC.Shell.SetTitleColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.UnselectedColor):
                    MC.Shell.SetUnselectedColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
            }
        }

        public override void Remove()
        {
            MC.Shell.SetNavBarIsVisible(Target, (bool)MC.Shell.NavBarIsVisibleProperty.DefaultValue);
            MC.Shell.SetNavBarHasShadow(Target, (bool)MC.Shell.NavBarHasShadowProperty.DefaultValue);
            MC.Shell.SetTabBarIsVisible(Target, (bool)MC.Shell.NavBarHasShadowProperty.DefaultValue);
            MC.Shell.SetBackgroundColor(Target, (Color)MC.Shell.BackgroundColorProperty.DefaultValue);
            MC.Shell.SetDisabledColor(Target, (Color)MC.Shell.DisabledColorProperty.DefaultValue);
            MC.Shell.SetForegroundColor(Target, (Color)MC.Shell.ForegroundColorProperty.DefaultValue);
            MC.Shell.SetTabBarBackgroundColor(Target, (Color)MC.Shell.TabBarBackgroundColorProperty.DefaultValue);
            MC.Shell.SetTabBarTitleColor(Target, (Color)MC.Shell.TabBarTitleColorProperty.DefaultValue);
            MC.Shell.SetTabBarUnselectedColor(Target, (Color)MC.Shell.TabBarUnselectedColorProperty.DefaultValue);
            MC.Shell.SetTitleColor(Target, (Color)MC.Shell.TitleColorProperty.DefaultValue);
            MC.Shell.SetUnselectedColor(Target, (Color)MC.Shell.UnselectedColorProperty.DefaultValue);
        }
    }
}
