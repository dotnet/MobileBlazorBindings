// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components.Rendering;

namespace Microsoft.MobileBlazorBindings.Core
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

        public void AddAttribute(string name, bool value)
        {
            // Using a fixed sequence number is allowed for attribute frames,
            // and causes the diff algorithm to use a dictionary to match old
            // and new values.

            // bool values are converted to ints (which later become strings) to ensure that
            // all values are always rendered, not only 'true' values. This ensures that the
            // element handlers will see all property changes and can handle them as needed.
            _underlyingBuilder.AddAttribute(0, name, value ? 1 : 0);
        }
    }
}
