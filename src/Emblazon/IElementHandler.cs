namespace Emblazon
{
    /// <summary>
    /// Represents an arbitrary element that Emblazon can create.
    /// </summary>
    public interface IElementHandler
    {
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
    }
}
