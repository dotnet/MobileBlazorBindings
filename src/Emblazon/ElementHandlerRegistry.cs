using System;
using System.Collections.Generic;

namespace Emblazon
{
    public static class ElementHandlerRegistry
    {
        internal static Dictionary<string, ElementHandlerFactory> ElementHandlers { get; }
            = new Dictionary<string, ElementHandlerFactory>();

        public static void RegisterElementHandler<TComponent>(
            Func<EmblazonRenderer, IElementHandler, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory(factory));
        }

        public static void RegisterElementHandler<TComponent>(
            Func<EmblazonRenderer, IElementHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory((renderer, _) => factory(renderer)));
        }

        public static void RegisterElementHandler<TComponent, TControlHandler>() where TComponent : NativeControlComponentBase where TControlHandler : class, IElementHandler, new()
        {
            RegisterElementHandler<TComponent>((_, __) => new TControlHandler());
        }
    }
}
