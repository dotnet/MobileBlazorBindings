using Microsoft.MobileBlazorBindings.Core;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class TemplatedPageHandler : PageHandler
    {
        public TemplatedPageHandler(NativeComponentRenderer renderer, XF.TemplatedPage templatedPageControl) : base(renderer, templatedPageControl)
        {
            TemplatedPageControl = templatedPageControl ?? throw new ArgumentNullException(nameof(templatedPageControl));
        }

        public XF.TemplatedPage TemplatedPageControl { get; }
    }
}
