using Microsoft.MobileBlazorBindings.Core;
using System;
using System.Linq;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class NavigableElementHandler : ElementHandler
    {
        public NavigableElementHandler(NativeComponentRenderer renderer, XF.NavigableElement navigableElementControl) : base(renderer, navigableElementControl)
        {
            NavigableElementControl = navigableElementControl ?? throw new ArgumentNullException(nameof(navigableElementControl));
        }

        public XF.NavigableElement NavigableElementControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.NavigableElement.StyleClass):
                    NavigableElementControl.StyleClass = ((string)attributeValue)?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
