using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class MasterDetailPage : Page
    {
        static MasterDetailPage()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<MasterDetailPage>(renderer => new MasterDetailPageHandler(renderer, new XF.MasterDetailPage()));
        }

        [Parameter] public string MasterTitle { get; set; }
        [Parameter] public XF.MasterBehavior? MasterBehavior { get; set; }

        [Parameter] public RenderFragment Master { get; set; }
        [Parameter] public RenderFragment Detail { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (MasterBehavior != null)
            {
                builder.AddAttribute(nameof(MasterBehavior), (int)MasterBehavior.Value);
            }
        }

#pragma warning disable CA1721 // Property names should not match get methods
        protected override RenderFragment GetChildContent() => RenderChildContent;
#pragma warning restore CA1721 // Property names should not match get methods

        private void RenderChildContent(RenderTreeBuilder builder)
        {
            builder.OpenComponent<MasterDetailMasterPage>(0);
            builder.AddAttribute(0, nameof(MasterDetailMasterPage.ChildContent), Master);
            // TODO: This feels a bit hacky. This is really a property of the child control, but here we are defining
            // it on the container control and applying it to the child. What about other such properties?
            builder.AddAttribute(1, "Title", MasterTitle);
            builder.CloseComponent();

            builder.OpenComponent<MasterDetailDetailPage>(1);
            builder.AddAttribute(0, nameof(MasterDetailDetailPage.ChildContent), Detail);
            builder.CloseComponent();
        }
    }
}
