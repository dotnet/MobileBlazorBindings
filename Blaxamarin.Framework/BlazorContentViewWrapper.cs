using Xamarin.Forms;

namespace Blaxamarin.Framework
{
    internal class BlazorContentViewWrapper : ContentView, IFormsControlHandler
    {
        public object NativeControl => this;
        public Element Element => this;

        // TODO: Need to think about whether this method is needed. There's no component for this element, so when
        // would this get called?
        public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            Elements.FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }
    }
}
