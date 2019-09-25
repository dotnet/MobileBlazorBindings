namespace Emblazon
{
    public interface IElementHandler
    {
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
        object NativeControl { get; }
    }
}
