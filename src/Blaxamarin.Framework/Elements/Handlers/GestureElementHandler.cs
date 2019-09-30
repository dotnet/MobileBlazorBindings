using Emblazon;
using XF=Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class GestureElementHandler : ElementHandler
    {
        public GestureElementHandler(EmblazonRenderer renderer, XF.GestureElement gestureElementControl) : base(renderer, gestureElementControl)
        {
            GestureElementControl = gestureElementControl ?? throw new System.ArgumentNullException(nameof(gestureElementControl));
        }

        public XF.GestureElement GestureElementControl { get; }
    }
}
