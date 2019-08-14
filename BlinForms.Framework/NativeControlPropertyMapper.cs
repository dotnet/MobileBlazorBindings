using BlinForms.Framework.Controls;

namespace BlinForms.Framework
{
    internal class NativeControlPropertyMapper : IControlPropertyMapper
    {
        public NativeControlPropertyMapper(IBlazorNativeControl nativeControl)
        {
            NativeControl = nativeControl;
        }

        public IBlazorNativeControl NativeControl { get; }

        public void SetControlProperty(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            // TODO: Does this need to return a 'bool' so we know if it actually set the property?
            NativeControl.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }
    }
}
