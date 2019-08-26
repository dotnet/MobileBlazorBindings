namespace Emblazon
{
    /// <summary>
    /// Utilities needed by Emblazon to managed native controls. Implementations
    /// of native rendering systems have their own quirks in terms of dealing with
    /// parent/child relationships, so each must implement this given the constraints
    /// and requirements of their systems.
    /// </summary>
    /// <typeparam name="TNativeComponent"></typeparam>
    public abstract class NativeControlManager<TNativeComponent> where TNativeComponent : class
    {
        public abstract bool IsParented(TNativeComponent nativeControl);
        public abstract void AddPhysicalControl(TNativeComponent parent, TNativeComponent child, int physicalSiblingIndex);
        public abstract int GetPhysicalSiblingIndex(TNativeComponent nativeComponent);
        public abstract void RemovePhysicalControl(TNativeComponent control);
    }
}
