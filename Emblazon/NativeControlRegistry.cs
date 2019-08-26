using System;
using System.Collections.Generic;

namespace Emblazon
{
    public static class NativeControlRegistry<TNativeComponent> where TNativeComponent : class
    {
        internal static Dictionary<string, ComponentControlFactory<TNativeComponent>> KnownElements { get; }
            = new Dictionary<string, ComponentControlFactory<TNativeComponent>>();

        public static void RegisterNativeControlComponent<TComponent>(Func<EmblazonRenderer<TNativeComponent>, TNativeComponent, TNativeComponent> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentControlFactory<TNativeComponent>(factory));
        }

        public static void RegisterNativeControlComponent<TComponent>(Func<EmblazonRenderer<TNativeComponent>, TNativeComponent> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentControlFactory<TNativeComponent>((renderer, _) => factory(renderer)));
        }

        public static void RegisterNativeControlComponent<TComponent, TControl>() where TComponent : NativeControlComponentBase where TControl : TNativeComponent, new()
        {
            RegisterNativeControlComponent<TComponent>((_, __) => new TControl());
        }
    }
}
