namespace Emblazon
{
    public interface IBlazorNativeControl
    {
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
    }
}
