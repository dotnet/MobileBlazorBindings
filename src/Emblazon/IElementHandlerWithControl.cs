namespace Emblazon
{
    /// <summary>
    /// Represents an element that is associated with a native control (typically a UI component).
    /// </summary>
    public interface IElementHandlerWithControl : IElementHandler
    {
        object NativeControl { get; }
    }
}
