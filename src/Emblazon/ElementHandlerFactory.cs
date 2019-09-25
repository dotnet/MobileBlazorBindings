using System;

namespace Emblazon
{
    internal class ElementHandlerFactory<TElementHandler> where TElementHandler : class, IElementHandler
    {
        private readonly Func<EmblazonRenderer<TElementHandler>, TElementHandler, TElementHandler> _callback;

        public ElementHandlerFactory(Func<EmblazonRenderer<TElementHandler>, TElementHandler, TElementHandler> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public TElementHandler CreateElementHandler(ElementHandlerFactoryContext<TElementHandler> context)
        {
            return _callback(context.Renderer, context.ParentHandler);
        }
    }
}
