using System;

namespace Emblazon
{
    internal class ComponentHandlerFactory<TControlHandler> where TControlHandler : class, INativeControlHandler
    {
        private readonly Func<EmblazonRenderer<TControlHandler>, TControlHandler, TControlHandler> _callback;

        public ComponentHandlerFactory(Func<EmblazonRenderer<TControlHandler>, TControlHandler, TControlHandler> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public TControlHandler CreateControl(ComponentControlFactoryContext<TControlHandler> context)
        {
            return _callback(context.Renderer, context.ParentHandler);
        }
    }
}
