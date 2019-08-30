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
        public abstract bool IsParented(TComponentHandler handler);
        public abstract void AddPhysicalControl(TComponentHandler parentHandler, TComponentHandler childHandler, int physicalSiblingIndex);
        public abstract int GetPhysicalSiblingIndex(TComponentHandler handler);
        public abstract void RemovePhysicalControl(TComponentHandler handler);
        public abstract bool IsParentOfChild(TComponentHandler parentHandler, TComponentHandler childHandler);
    }
}
