// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;

namespace Microsoft.MobileBlazorBindings.Core
{
    internal class ElementHandlerFactoryContext
    {
        public ElementHandlerFactoryContext(NativeComponentRenderer renderer, IElementHandler parentHandler, IComponent component)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ParentHandler = parentHandler;
            Component = component;
        }

        public IElementHandler ParentHandler { get; }
        public IComponent Component { get; }
        public NativeComponentRenderer Renderer { get; }
    }
}
