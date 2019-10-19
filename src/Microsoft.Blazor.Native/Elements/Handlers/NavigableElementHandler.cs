using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class NavigableElementHandler : ElementHandler
    {
        public NavigableElementHandler(EmblazonRenderer renderer, XF.NavigableElement navigableElementControl) : base(renderer, navigableElementControl)
        {
            NavigableElementControl = navigableElementControl ?? throw new System.ArgumentNullException(nameof(navigableElementControl));
        }

        public XF.NavigableElement NavigableElementControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case "__CloseDialog":
                    if (attributeValue != null)
                    {
                        NavigableElementControl.Navigation.PopModalAsync();
                    }
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
