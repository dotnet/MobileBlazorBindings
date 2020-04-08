using BlazorDesktop.Elements;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace BlazorDesktop.Components.Handlers
{
    public class BlazorWebViewHandler : ViewHandler
    {
        public BlazorWebViewHandler(NativeComponentRenderer renderer, Elements.BlazorWebView control)
            : base(renderer, control)
        {
        }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case "BlazorWebViewId":
                    var component = Components.BlazorWebView.FindById(long.Parse((string)attributeValue));
                    ((Elements.BlazorWebView)ViewControl).Init(component.Services, Renderer.Dispatcher);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
