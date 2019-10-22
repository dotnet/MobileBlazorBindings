using Emblazon;
using System;
using Xamarin.Forms;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ImageHandler : ViewHandler
    {
        public ImageHandler(EmblazonRenderer renderer, XF.Image imageControl) : base(renderer, imageControl)
        {
            ImageControl = imageControl ?? throw new ArgumentNullException(nameof(imageControl));
        }

        public XF.Image ImageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Image.Aspect):
                    ImageControl.Aspect = (Aspect)AttributeHelper.GetInt(attributeValue);
                    break;
                case nameof(XF.Image.IsOpaque):
                    ImageControl.IsOpaque = AttributeHelper.GetBool(attributeValue);
                    break;
                case nameof(XF.Image.Source) + "_AsFile":
                    ImageControl.Source = new XF.FileImageSource { File = (string)attributeValue };
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
