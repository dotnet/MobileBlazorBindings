using System;

namespace Microsoft.MobileBlazorBindings.Core
{
    internal class ElementHandlerFactory
    {
        private readonly Func<NativeComponentRenderer, IElementHandler, IElementHandler> _callback;

        public ElementHandlerFactory(Func<NativeComponentRenderer, IElementHandler, IElementHandler> callback)
        {
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
        }

        public IElementHandler CreateElementHandler(ElementHandlerFactoryContext context)
        {
            return _callback(context.Renderer, context.ParentHandler);
        }
    }
}
