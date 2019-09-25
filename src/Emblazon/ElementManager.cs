namespace Emblazon
{
    /// <summary>
    /// Utilities needed by Emblazon to managed native controls. Implementations
    /// of native rendering systems have their own quirks in terms of dealing with
    /// parent/child relationships, so each must implement this given the constraints
    /// and requirements of their systems.
    /// </summary>
    /// <typeparam name="TElementHandler"></typeparam>
    public abstract class ElementManager<TElementHandler> where TElementHandler : IElementHandler
    {
        public abstract bool IsParented(TElementHandler handler);
        public abstract void AddChildElement(TElementHandler parentHandler, TElementHandler childHandler, int physicalSiblingIndex);
        public abstract int GetPhysicalSiblingIndex(TElementHandler handler);
        public abstract void RemoveElement(TElementHandler handler);
        public abstract bool IsParentOfChild(TElementHandler parentHandler, TElementHandler childHandler);
    }
}
