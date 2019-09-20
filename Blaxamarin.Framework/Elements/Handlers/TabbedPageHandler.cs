using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class TabbedPageHandler : PageHandler
    {
        public TabbedPageHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.TabbedPage tabbedPageControl) : base(renderer, tabbedPageControl)
        {
            TabbedPageControl = tabbedPageControl ?? throw new System.ArgumentNullException(nameof(tabbedPageControl));
        }

        public XF.TabbedPage TabbedPageControl { get; }
    }
}
