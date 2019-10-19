using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class PageHandler : VisualElementHandler
    {
        public PageHandler(EmblazonRenderer renderer, XF.Page pageControl) : base(renderer, pageControl)
        {
            PageControl = pageControl ?? throw new ArgumentNullException(nameof(pageControl));
        }

        public XF.Page PageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Page.IconImageSource) + "_AsFile":
                    PageControl.IconImageSource = new XF.FileImageSource { File = (string)attributeValue };
                    break;
                case nameof(XF.Page.Title):
                    PageControl.Title = (string)attributeValue;
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
