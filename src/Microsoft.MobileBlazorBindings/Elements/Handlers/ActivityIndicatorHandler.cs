using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class ActivityIndicatorHandler : ViewHandler
    {
        public ActivityIndicatorHandler(EmblazonRenderer renderer, XF.ActivityIndicator activityIndicatorControl) : base(renderer, activityIndicatorControl)
        {
            ActivityIndicatorControl = activityIndicatorControl ?? throw new ArgumentNullException(nameof(activityIndicatorControl));
        }

        public XF.ActivityIndicator ActivityIndicatorControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(ActivityIndicator.IsRunning):
                    ActivityIndicatorControl.IsRunning = (bool)(attributeValue ?? (bool)XF.ActivityIndicator.IsRunningProperty.DefaultValue);
                    break;
                case nameof(ActivityIndicator.Color):
                    ActivityIndicatorControl.Color = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
