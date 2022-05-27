// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Microsoft.MobileBlazorBindings.Core
{
    public static class ElementHandlerRegistry
    {
        internal static Dictionary<string, ElementHandlerFactory> ElementHandlers { get; }
            = new Dictionary<string, ElementHandlerFactory>();

        public static void RegisterElementHandler<TComponent>(
            Func<NativeComponentRenderer, IElementHandler, TComponent, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory((renderer, parent, component) => factory(renderer, parent, (TComponent)component)));
        }

        public static void RegisterElementHandler<TComponent>(
            Func<NativeComponentRenderer, IElementHandler, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory((renderer, parent, _) => factory(renderer, parent)));
        }

        public static void RegisterElementHandler<TComponent>(
            Func<NativeComponentRenderer, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory((renderer, _, __) => factory(renderer)));
        }

        public static void RegisterPropertyContentHandler<TComponent>(string propertyName,
            Func<NativeComponentRenderer, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            var key = $"p-{typeof(TComponent).FullName}.{propertyName}";
            ElementHandlers.Add(key, new ElementHandlerFactory((renderer, _, __) => factory(renderer)));
        }

        public static void RegisterPropertyContentHandler<TComponent>(string propertyName,
            Func<NativeComponentRenderer, IElementHandler, IComponent, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            var key = $"p-{typeof(TComponent).FullName}.{propertyName}";
            ElementHandlers.Add(key, new ElementHandlerFactory((renderer, parent, component) => factory(renderer, parent, component)));
        }

        public static void RegisterElementHandler<TComponent, TControlHandler>() where TComponent : NativeControlComponentBase where TControlHandler : class, IElementHandler, new()
        {
            RegisterElementHandler<TComponent>((_, __) => new TControlHandler());
        }
    }
}
