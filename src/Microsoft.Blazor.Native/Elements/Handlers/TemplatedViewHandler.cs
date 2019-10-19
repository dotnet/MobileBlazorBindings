using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public class TemplatedViewHandler : LayoutHandler
    {
        public TemplatedViewHandler(EmblazonRenderer renderer, XF.TemplatedView templatedViewControl) : base(renderer, templatedViewControl)
        {
            TemplatedViewControl = templatedViewControl ?? throw new ArgumentNullException(nameof(templatedViewControl));
        }

        public XF.TemplatedView TemplatedViewControl { get; }
    }
}
