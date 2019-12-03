using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements.Handlers
{
    public class MasterDetailPageHandler : PageHandler
    {
        public MasterDetailPageHandler(EmblazonRenderer renderer, XF.MasterDetailPage masterDetailPageControl) : base(renderer, masterDetailPageControl)
        {
            MasterDetailPageControl = masterDetailPageControl ?? throw new ArgumentNullException(nameof(masterDetailPageControl));

            // Set dummy Master and Detail because this element cannot be parented unless both are set.
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L199
            // In Blazor, parents are created before children, whereas this doesn't appear to be the case in
            // Xamarin.Forms. Once the Blazor children get created, they will overwrite these dummy elements.

            // The Master page must have its Title set:
            // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
            MasterDetailPageControl.Master = new XF.Page() { Title = "Title" };
            MasterDetailPageControl.Detail = new XF.Page();
        }

        public XF.MasterDetailPage MasterDetailPageControl { get; }

        public override void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            switch (attributeName)
            {
                case nameof(XF.MasterDetailPage.MasterBehavior):
                    MasterDetailPageControl.MasterBehavior = (XF.MasterBehavior)AttributeHelper.GetInt(attributeValue);
                    break;
                default:
                    base.ApplyAttribute(attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }
    }
}
