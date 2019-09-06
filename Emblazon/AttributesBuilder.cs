using Microsoft.AspNetCore.Components.RenderTree;

namespace Emblazon
{
    // This wraps a RenderTreeBuilder in such a way that consumers
    // can only call the desired AddAttribute method, can't supply
    // sequence numbers, and can't leak the instance outside their
    // position in the call stack.

    public readonly ref struct AttributesBuilder
    {
        private readonly RenderTreeBuilder _underlyingBuilder;

        public AttributesBuilder(RenderTreeBuilder underlyingBuilder)
        {
            _underlyingBuilder = underlyingBuilder;
        }

        public void AddAttribute(string name, object value)
        {
            // Using a fixed sequence number is allowed for attribute frames,
            // and causes the diff algorithm to use a dictionary to match old
            // and new values.
            _underlyingBuilder.AddAttribute(0, name, value);
        }
    }
}
