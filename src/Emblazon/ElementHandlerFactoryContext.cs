using System;

namespace Emblazon
{
    internal class ElementHandlerFactoryContext<TElementHandler> where TElementHandler : class, IElementHandler
    {
        public ElementHandlerFactoryContext(EmblazonRenderer<TElementHandler> renderer, TElementHandler parentHandler)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ParentHandler = parentHandler;
        }

        public TElementHandler ParentHandler { get; }

        public EmblazonRenderer<TElementHandler> Renderer { get; }
    }
}
