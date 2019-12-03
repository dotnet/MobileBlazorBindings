using Emblazon;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class TabbedPageHandler : PageHandler
    {
        public TabbedPageHandler(EmblazonRenderer renderer, XF.TabbedPage tabbedPageControl) : base(renderer, tabbedPageControl)
        {
            TabbedPageControl = tabbedPageControl ?? throw new System.ArgumentNullException(nameof(tabbedPageControl));
        }

        public XF.TabbedPage TabbedPageControl { get; }
    }
}
