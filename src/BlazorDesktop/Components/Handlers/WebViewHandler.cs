using BlazorDesktop.Elements;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Components.Handlers
{
    public class WebViewHandler : ViewHandler
    {
        public WebViewExtended Control { get; }

        public WebViewHandler(NativeComponentRenderer renderer, WebViewExtended control)
            : base(renderer, control)
        {
            Control = control;
        }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.HtmlWebViewSource):
                    Control.Source = new XF.HtmlWebViewSource { Html = (string)attributeValue };
                    break;
                case nameof(XF.UrlWebViewSource):
                    Control.Source = new XF.UrlWebViewSource { Url = (string)attributeValue };
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
