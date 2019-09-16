using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class MasterDetailPage : FormsComponentBase
    {
        static MasterDetailPage()
        {
            NativeControlRegistry<IFormsControlHandler>
                .RegisterNativeControlComponent<MasterDetailPage>(renderer => new BlazorMasterDetailPage(renderer));
        }

        [Parameter] public string MasterTitle { get; set; }
        [Parameter] public MasterBehavior? MasterBehavior { get; set; }

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

        protected override RenderFragment GetChildContent() => RenderChildContent;

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

        class BlazorMasterDetailPage : Xamarin.Forms.MasterDetailPage, IFormsControlHandler
        {
            public BlazorMasterDetailPage(EmblazonRenderer<IFormsControlHandler> renderer)
            {
                Renderer = renderer;

                // Set dummy Master and Detail because this element cannot be parented unless both are set.
                // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L199
                // In Blazor, parents are created before children, whereas this doesn't appear to be the case in
                // Xamarin.Forms. Once the Blazor children get created, they will overwrite these dummy elements.

                // The Master page must have its Title set:
                // https://github.com/xamarin/Xamarin.Forms/blob/ff63ef551d9b2b5736092eb48aaf954f54d63417/Xamarin.Forms.Core/MasterDetailPage.cs#L72
                Master = new Page() { Title = "Title" };
                Detail = new Page();
            }

            public EmblazonRenderer<IFormsControlHandler> Renderer { get; }
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(MasterBehavior):
                        MasterBehavior = (MasterBehavior)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
