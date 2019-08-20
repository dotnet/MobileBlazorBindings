namespace Emblazon
{
    internal interface IControlPropertyMapper
    {
        void SetControlProperty(ulong attributeEventHandlerId,
                                string attributeName,
                                object attributeValue,
                                string attributeEventUpdatesAttributeName);
    }
}
