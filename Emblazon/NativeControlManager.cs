namespace Emblazon
{
    /// <summary>
    /// Utilities needed by Emblazon to managed native controls. Implementations
    /// of native rendering systems have their own quirks in terms of dealing with
    /// parent/child relationships, so each must implement this given the constraints
    /// and requirements of their systems.
    /// </summary>
    /// <typeparam name="TComponentHandler"></typeparam>
    public abstract class NativeControlManager<TComponentHandler> where TComponentHandler : INativeControlHandler
    {
        public abstract bool IsParented(TComponentHandler nativeControl);
        public abstract void AddPhysicalControl(TComponentHandler parent, TComponentHandler child, int physicalSiblingIndex);
        public abstract int GetPhysicalSiblingIndex(TComponentHandler nativeComponent);
        public abstract void RemovePhysicalControl(TComponentHandler control);
        public abstract bool IsParentOfChild(TComponentHandler parentControl, TComponentHandler childControl);
    }
}
