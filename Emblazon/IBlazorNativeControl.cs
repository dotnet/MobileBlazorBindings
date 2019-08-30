namespace Emblazon
{
    public interface IBlazorNativeControl<out T>
    {
        void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName);
        T NativeControl { get; }
    }
}
