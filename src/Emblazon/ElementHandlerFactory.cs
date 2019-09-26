using System;

namespace Emblazon
{
    internal class ElementHandlerFactory
    {
        private readonly Func<EmblazonRenderer, IElementHandler, IElementHandler> _callback;

        public ElementHandlerFactory(Func<EmblazonRenderer, IElementHandler, IElementHandler> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public IElementHandler CreateElementHandler(ElementHandlerFactoryContext context)
        {
            return _callback(context.Renderer, context.ParentHandler);
        }
    }
}
