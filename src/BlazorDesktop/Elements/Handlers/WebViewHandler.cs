using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using Xamarin.Forms;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Elements.Handlers
{
    internal class WebViewHandler : ViewHandler
    {
        public XF.WebView Control { get; }

        public WebViewHandler(NativeComponentRenderer renderer, XF.WebView control)
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
