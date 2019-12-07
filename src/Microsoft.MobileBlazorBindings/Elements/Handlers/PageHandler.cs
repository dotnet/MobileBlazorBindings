using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class PageHandler : VisualElementHandler
    {
        public PageHandler(NativeComponentRenderer renderer, XF.Page pageControl) : base(renderer, pageControl)
        {
            PageControl = pageControl ?? throw new ArgumentNullException(nameof(pageControl));
        }

        public XF.Page PageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Page.IconImageSource):
                    PageControl.IconImageSource = attributeValue == null ? null : AttributeHelper.StringToImageSource((string)attributeValue);
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
