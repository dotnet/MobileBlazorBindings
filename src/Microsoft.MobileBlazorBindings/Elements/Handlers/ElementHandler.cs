using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class ElementHandler : IXamarinFormsElementHandler
    {
        public ElementHandler(EmblazonRenderer renderer, XF.Element elementControl)
        {
            Renderer = renderer ?? throw new ArgumentNullException(nameof(renderer));
            ElementControl = elementControl ?? throw new ArgumentNullException(nameof(elementControl));
        }

        public EmblazonRenderer Renderer { get; }
        public XF.Element ElementControl { get; }
        public object TargetElement => ElementControl;

        public virtual void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.Element.StyleId):
                    ElementControl.StyleId = (string)attributeValue;
                    break;
                default:
                    throw new NotImplementedException($"{GetType().FullName} doesn't recognize attribute '{attributeName}'");
            }
        }
    }
}
