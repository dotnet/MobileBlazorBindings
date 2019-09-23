using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements.Handlers
{
    public class ContentPageHandler : TemplatedPageHandler
    {
        public ContentPageHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.ContentPage contentPageControl) : base(renderer, contentPageControl)
        {
            ContentPageControl = contentPageControl ?? throw new ArgumentNullException(nameof(contentPageControl));
        }

        public XF.ContentPage ContentPageControl { get; }
    }
}
