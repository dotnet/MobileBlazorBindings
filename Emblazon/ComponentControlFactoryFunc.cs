using System;

namespace Emblazon
{
    public class ComponentControlFactoryFunc<TNativeComponent> : IComponentControlFactory<TNativeComponent> where TNativeComponent : class
    {
        private readonly Func<EmblazonRenderer<TNativeComponent>, TNativeComponent, TNativeComponent> _callback;

        public ComponentControlFactoryFunc(Func<EmblazonRenderer<TNativeComponent>, TNativeComponent, TNativeComponent> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public TNativeComponent CreateControl(ComponentControlFactoryContext<TNativeComponent> context)
        {
            return _callback(context.Renderer, context.ParentControl);
        }
    }
}
