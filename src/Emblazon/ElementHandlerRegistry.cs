using System;
using System.Collections.Generic;

namespace Emblazon
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    public static class ElementHandlerRegistry<TNativeControlHandler> where TNativeControlHandler : class, IElementHandler
    {
        internal static Dictionary<string, ElementHandlerFactory<TNativeControlHandler>> ElementHandlers { get; }
            = new Dictionary<string, ElementHandlerFactory<TNativeControlHandler>>();

        public static void RegisterElementHandler<TComponent>(
            Func<EmblazonRenderer<TNativeControlHandler>, TNativeControlHandler, TNativeControlHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory<TNativeControlHandler>(factory));
        }

        public static void RegisterElementHandler<TComponent>(
            Func<EmblazonRenderer<TNativeControlHandler>, TNativeControlHandler> factory) where TComponent : NativeControlComponentBase
        {
            ElementHandlers.Add(typeof(TComponent).FullName, new ElementHandlerFactory<TNativeControlHandler>((renderer, _) => factory(renderer)));
        }

        public static void RegisterElementHandler<TComponent, TControlHandler>() where TComponent : NativeControlComponentBase where TControlHandler : class, IElementHandler, new()
        {
            RegisterElementHandler<TComponent>((_, __) => new TControlHandler() as TNativeControlHandler);
        }
    }
#pragma warning restore CA1000 // Do not declare static members on generic types
}
