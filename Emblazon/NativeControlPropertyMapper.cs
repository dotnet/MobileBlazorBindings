namespace Emblazon
{
    internal class NativeControlPropertyMapper : IControlPropertyMapper
    {
        public NativeControlPropertyMapper(INativeControlHandler nativeControl)
        {
            NativeControl = nativeControl;
        }

        public INativeControlHandler NativeControl { get; }

        public void SetControlProperty(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            // TODO: Does this need to return a 'bool' so we know if it actually set the property?
            NativeControl.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
        }
    }
}
