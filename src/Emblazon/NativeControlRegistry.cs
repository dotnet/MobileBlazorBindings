using System;
using System.Collections.Generic;

namespace Emblazon
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    public static class NativeControlRegistry<TNativeControlHandler> where TNativeControlHandler : class, INativeControlHandler
    {
        internal static Dictionary<string, ComponentHandlerFactory<TNativeControlHandler>> KnownElements { get; }
            = new Dictionary<string, ComponentHandlerFactory<TNativeControlHandler>>();

        public static void RegisterNativeControlComponent<TComponent>(
            Func<EmblazonRenderer<TNativeControlHandler>, TNativeControlHandler, TNativeControlHandler> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentHandlerFactory<TNativeControlHandler>(factory));
        }

        public static void RegisterNativeControlComponent<TComponent>(
            Func<EmblazonRenderer<TNativeControlHandler>, TNativeControlHandler> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentHandlerFactory<TNativeControlHandler>((renderer, _) => factory(renderer)));
        }

        public static void RegisterNativeControlComponent<TComponent, TControlHandler>() where TComponent : NativeControlComponentBase where TControlHandler : class, INativeControlHandler, new()
        {
            RegisterNativeControlComponent<TComponent>((_, __) => new TControlHandler() as TNativeControlHandler);
        }
    }
#pragma warning restore CA1000 // Do not declare static members on generic types
}
