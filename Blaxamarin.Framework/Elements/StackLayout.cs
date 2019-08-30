using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class StackLayout : FormsComponentBase
    {
        static StackLayout()
        {
            NativeControlRegistry<Element>.RegisterNativeControlComponent<StackLayout, BlazorStackLayout>();
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public StackOrientation? Orientation { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        class BlazorStackLayout : Xamarin.Forms.StackLayout, IBlazorNativeControl<Xamarin.Forms.StackLayout>
        {
            public Xamarin.Forms.StackLayout NativeControl => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Orientation):
                        Orientation = (StackOrientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
