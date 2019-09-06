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
<<<<<<< HEAD
        public abstract bool IsParented(TComponentHandler handler);
        public abstract void AddPhysicalControl(TComponentHandler parentHandler, TComponentHandler childHandler, int physicalSiblingIndex);
        public abstract int GetPhysicalSiblingIndex(TComponentHandler handler);
        public abstract void RemovePhysicalControl(TComponentHandler handler);
=======
        public abstract bool IsParented(TNativeComponent nativeControl);
        public abstract void AddPhysicalControl(TNativeComponent parent, TNativeComponent child, int physicalSiblingIndex);
        public abstract int GetPhysicalSiblingIndex(TNativeComponent nativeComponent);
        public abstract void RemovePhysicalControl(TNativeComponent control);
        public abstract bool IsParentOfChild(TNativeComponent parentControl, TNativeComponent childControl);
>>>>>>> abd3d233d47af015f86ca491d516b9604c8d9953
    }
}
