using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class NavigableElementHandler : ElementHandler
    {
        public NavigableElementHandler(EmblazonRenderer renderer, XF.NavigableElement navigableElementControl) : base(renderer, navigableElementControl)
        {
            NavigableElementControl = navigableElementControl ?? throw new System.ArgumentNullException(nameof(navigableElementControl));
        }

        public XF.NavigableElement NavigableElementControl { get; }
    }
}
