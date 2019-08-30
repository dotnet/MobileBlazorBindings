namespace Emblazon
{
    public interface INativeControlHandler
    {
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
        object NativeControl { get; }
    }
}
