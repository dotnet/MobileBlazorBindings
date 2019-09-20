using Emblazon;
using XF = Xamarin.Forms;
namespace Blaxamarin.Framework.Elements.Handlers
{
    public sealed class DetailPageHandler : ContentPageHandler
    {
        public DetailPageHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.ContentPage masterDetailPageControl) : base(renderer, masterDetailPageControl)
        {
            MasterDetailPageControl = masterDetailPageControl ?? throw new System.ArgumentNullException(nameof(masterDetailPageControl));
        }

        public XF.ContentPage MasterDetailPageControl { get; }
    }
}
