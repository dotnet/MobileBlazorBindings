using System;
using System.Collections.Generic;

namespace Emblazon
{
    public static class NativeControlRegistry<TNativeComponent> where TNativeComponent : class
    {
        internal static Dictionary<string, ComponentControlFactory<IBlazorNativeControl<TNativeComponent>>> KnownElements { get; }
            = new Dictionary<string, ComponentControlFactory<IBlazorNativeControl<TNativeComponent>>>();

        public static void RegisterNativeControlComponent<TComponent>(
            Func<EmblazonRenderer<IBlazorNativeControl<TNativeComponent>>, IBlazorNativeControl<TNativeComponent>, IBlazorNativeControl<TNativeComponent>> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentControlFactory<IBlazorNativeControl<TNativeComponent>>(factory));
        }

        public static void RegisterNativeControlComponent<TComponent>(
            Func<EmblazonRenderer<IBlazorNativeControl<TNativeComponent>>, IBlazorNativeControl<TNativeComponent>> factory) where TComponent : NativeControlComponentBase
        {
            KnownElements.Add(typeof(TComponent).FullName, new ComponentControlFactory<IBlazorNativeControl<TNativeComponent>>((renderer, _) => factory(renderer)));
        }

        public static void RegisterNativeControlComponent<TComponent, TControl>() where TComponent : NativeControlComponentBase where TControl : TNativeComponent, IBlazorNativeControl<TNativeComponent>, new()
        {
            RegisterNativeControlComponent<TComponent>((_, __) => new TControl());
        }
    }
}
