// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using System;
using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings.Core
{
    public static class ElementHandlerRegistry
    {
        internal static Dictionary<string, ElementHandlerFactory> ElementHandlers { get; }
            = new Dictionary<string, ElementHandlerFactory>();

        public static void RegisterElementHandler<TComponent>(
            Func<NativeComponentRenderer, IElementHandler, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory(factory));
        }

        public static void RegisterElementHandler<TComponent>(
            Func<NativeComponentRenderer, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory((renderer, _) => factory(renderer)));
        }

        public static void RegisterElementHandler<TComponent, TControlHandler>() where TComponent : NativeControlComponentBase where TControlHandler : class, IElementHandler, new()
        {
            RegisterElementHandler<TComponent>((_, __) => new TControlHandler());
        }
    }
}
