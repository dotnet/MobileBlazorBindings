using System;

namespace Emblazon
{
    internal class ElementHandlerFactoryContext
    {
        public ElementHandlerFactoryContext(EmblazonRenderer renderer, IElementHandler parentHandler)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ParentHandler = parentHandler;
        }

        public IElementHandler ParentHandler { get; }

        public EmblazonRenderer Renderer { get; }
    }
}
