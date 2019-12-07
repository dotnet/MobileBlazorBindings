using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class TemplatedViewHandler : LayoutHandler
    {
        public TemplatedViewHandler(NativeComponentRenderer renderer, XF.TemplatedView templatedViewControl) : base(renderer, templatedViewControl)
        {
            TemplatedViewControl = templatedViewControl ?? throw new ArgumentNullException(nameof(templatedViewControl));
        }

        public XF.TemplatedView TemplatedViewControl { get; }
    }
}
