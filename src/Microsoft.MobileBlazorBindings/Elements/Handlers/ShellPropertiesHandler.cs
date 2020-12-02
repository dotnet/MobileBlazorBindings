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
                    XF.Shell.SetNavBarIsVisible(Parent, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.NavBarHasShadow):
                    XF.Shell.SetNavBarHasShadow(Parent, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarIsVisible):
                    XF.Shell.SetTabBarIsVisible(Parent, AttributeHelper.GetBool(attributeValue));
                    break;
                case nameof(ShellProperties.BackgroundColor):
                    XF.Shell.SetBackgroundColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.DisabledColor):
                    XF.Shell.SetDisabledColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.ForegroundColor):
                    XF.Shell.SetForegroundColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarBackgroundColor):
                    XF.Shell.SetTabBarBackgroundColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarDisabledColor):
                    XF.Shell.SetTabBarDisabledColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarForegroundColor):
                    XF.Shell.SetTabBarForegroundColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarTitleColor):
                    XF.Shell.SetTabBarTitleColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TabBarUnselectedColor):
                    XF.Shell.SetTabBarUnselectedColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.TitleColor):
                    XF.Shell.SetTitleColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
                case nameof(ShellProperties.UnselectedColor):
                    XF.Shell.SetUnselectedColor(Parent, AttributeHelper.StringToColor(attributeValue));
                    break;
            }
        }
    }
}
