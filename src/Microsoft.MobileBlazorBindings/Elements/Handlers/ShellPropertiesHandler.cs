// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ShellPropertiesHandler : BaseAttachedPropertiesHandler
    {
        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(ShellProperties.NavBarIsVisible):
                    XF.Shell.SetNavBarIsVisible(Target, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.NavBarHasShadow):
                    XF.Shell.SetNavBarHasShadow(Target, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarIsVisible):
                    XF.Shell.SetTabBarIsVisible(Target, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.BackgroundColor):
                    XF.Shell.SetBackgroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.DisabledColor):
                    XF.Shell.SetDisabledColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.ForegroundColor):
                    XF.Shell.SetForegroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarBackgroundColor):
                    XF.Shell.SetTabBarBackgroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarDisabledColor):
                    XF.Shell.SetTabBarDisabledColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarForegroundColor):
                    XF.Shell.SetTabBarForegroundColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarTitleColor):
                    XF.Shell.SetTabBarTitleColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarUnselectedColor):
                    XF.Shell.SetTabBarUnselectedColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TitleColor):
                    XF.Shell.SetTitleColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.UnselectedColor):
                    XF.Shell.SetUnselectedColor(Target, AttributeHelper.StringToColor(attributeValue));
                    break;
            }
        }

        public override void Remove()
        {
            XF.Shell.SetNavBarIsVisible(Target, (bool)XF.Shell.NavBarIsVisibleProperty.DefaultValue);
            XF.Shell.SetNavBarHasShadow(Target, (bool)XF.Shell.NavBarHasShadowProperty.DefaultValue);
            XF.Shell.SetTabBarIsVisible(Target, (bool)XF.Shell.NavBarHasShadowProperty.DefaultValue);
            XF.Shell.SetBackgroundColor(Target, (XF.Color)XF.Shell.BackgroundColorProperty.DefaultValue);
            XF.Shell.SetDisabledColor(Target, (XF.Color)XF.Shell.DisabledColorProperty.DefaultValue);
            XF.Shell.SetForegroundColor(Target, (XF.Color)XF.Shell.ForegroundColorProperty.DefaultValue);
            XF.Shell.SetTabBarBackgroundColor(Target, (XF.Color)XF.Shell.TabBarBackgroundColorProperty.DefaultValue);
            XF.Shell.SetTabBarTitleColor(Target, (XF.Color)XF.Shell.TabBarTitleColorProperty.DefaultValue);
            XF.Shell.SetTabBarUnselectedColor(Target, (XF.Color)XF.Shell.TabBarUnselectedColorProperty.DefaultValue);
            XF.Shell.SetTitleColor(Target, (XF.Color)XF.Shell.TitleColorProperty.DefaultValue);
            XF.Shell.SetUnselectedColor(Target, (XF.Color)XF.Shell.UnselectedColorProperty.DefaultValue);
        }
    }
}
