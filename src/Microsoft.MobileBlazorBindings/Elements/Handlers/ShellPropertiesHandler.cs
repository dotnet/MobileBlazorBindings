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
    }
}
