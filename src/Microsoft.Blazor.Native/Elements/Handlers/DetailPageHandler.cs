using Emblazon;
using XF = Xamarin.Forms;
namespace Microsoft.Blazor.Native.Elements.Handlers
{
    public sealed class DetailPageHandler : ContentPageHandler
    {
        public DetailPageHandler(EmblazonRenderer renderer, XF.ContentPage masterDetailPageControl) : base(renderer, masterDetailPageControl)
        {
            MasterDetailPageControl = masterDetailPageControl ?? throw new System.ArgumentNullException(nameof(masterDetailPageControl));
        }

        public XF.ContentPage MasterDetailPageControl { get; }
    }
}
