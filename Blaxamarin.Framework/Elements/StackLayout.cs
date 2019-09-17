using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class StackLayout : FormsComponentBase
    {
        static StackLayout()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<StackLayout, BlazorStackLayout>();
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
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

        class BlazorStackLayout : Xamarin.Forms.StackLayout, IFormsControlHandler
        {
            public object NativeControl => this;
            public Element Element => this;
            
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
