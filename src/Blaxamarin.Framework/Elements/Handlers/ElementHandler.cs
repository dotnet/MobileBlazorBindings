using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class ElementHandler : IXamarinFormsElementHandler
    {
        public ElementHandler(EmblazonRenderer<IXamarinFormsElementHandler> renderer, XF.Element elementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ElementControl = elementControl ?? throw new ArgumentNullException(nameof(elementControl));
        }

        public EmblazonRenderer<IXamarinFormsElementHandler> Renderer { get; }
        public XF.Element ElementControl { get; }
        public object NativeControl => ElementControl;

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
