using Emblazon;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class LayoutHandler : ViewHandler
    {
        public LayoutHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.Layout layoutControl) : base(renderer, layoutControl)
        {
            LayoutControl = layoutControl ?? throw new System.ArgumentNullException(nameof(layoutControl));
        }

        public XF.Layout LayoutControl { get; }
    }
}
