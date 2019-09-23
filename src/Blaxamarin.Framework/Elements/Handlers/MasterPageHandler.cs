using Emblazon;
using XF = Xamarin.Forms;
namespace Blaxamarin.Framework.Elements.Handlers
{
    public sealed class MasterPageHandler : ContentPageHandler
    {
        public MasterPageHandler(EmblazonRenderer<IFormsControlHandler> renderer, XF.ContentPage masterDetailPageControl) : base(renderer, masterDetailPageControl)
        {
            MasterDetailPageControl = masterDetailPageControl ?? throw new System.ArgumentNullException(nameof(masterDetailPageControl));

            // The Master page must have its Title set:
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
            ContentPageControl.Title = "Title";
        }

        public XF.ContentPage MasterDetailPageControl { get; }
    }
}
