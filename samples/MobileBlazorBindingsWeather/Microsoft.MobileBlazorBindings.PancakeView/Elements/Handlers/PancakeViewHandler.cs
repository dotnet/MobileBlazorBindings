using Microsoft.MobileBlazorBindings.Elements;
using System;
using XF = Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Emblazon;
using Microsoft.MobileBlazorBindings.Elements.Handlers;

namespace Microsoft.MobileBlazorBindings.PancakeView.Elements
{
    public class PancakeViewHandler : ContentViewHandler
    {
        public PancakeViewHandler(EmblazonRenderer renderer, XF.PancakeView.PancakeView pancakeViewControl) : base(renderer, pancakeViewControl)
        {
            PancakeViewControl = pancakeViewControl ?? throw new ArgumentNullException(nameof(pancakeViewControl));
        }

        public XF.PancakeView.PancakeView PancakeViewControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.PancakeView.PancakeView.BackgroundGradientAngle):
                    PancakeViewControl.BackgroundGradientAngle = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BackgroundGradientEndColor):
                    PancakeViewControl.BackgroundGradientEndColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BackgroundGradientStartColor):
                    PancakeViewControl.BackgroundGradientStartColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                //[Parameter] public IEnumerable<GradientStop> BackgroundGradientStops { get; set; }
                case nameof(XF.PancakeView.PancakeView.BorderColor):
                    PancakeViewControl.BorderColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BorderDrawingStyle):
                    PancakeViewControl.BorderDrawingStyle = (BorderDrawingStyle)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BorderGradientAngle):
                    PancakeViewControl.BorderGradientAngle = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BorderGradientEndColor):
                    PancakeViewControl.BorderGradientEndColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BorderGradientStartColor):
                    PancakeViewControl.BorderGradientStartColor = AttributeHelper.StringToColor((string)attributeValue);
                    break;
                //[Parameter] public IEnumerable<GradientStop> BorderGradientStops { get; set; }
                case nameof(XF.PancakeView.PancakeView.BorderIsDashed):
                    PancakeViewControl.BorderIsDashed = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.BorderThickness):
                    PancakeViewControl.BorderThickness = AttributeHelper.StringToSingle((string)attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.CornerRadius):
                    PancakeViewControl.CornerRadius = AttributeHelper.StringToCornerRadius(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.Elevation):
                    PancakeViewControl.Elevation = AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.HasShadow):
                    PancakeViewControl.HasShadow = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.OffsetAngle):
                    PancakeViewControl.OffsetAngle = AttributeHelper.StringToDouble((string)attributeValue);
                    break;
                case nameof(XF.PancakeView.PancakeView.Sides):
                    PancakeViewControl.Sides = AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
