using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ScrollView : FormsComponentBase
    {
        static ScrollView()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<ScrollView, BlazorScrollView>();
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
        [Parameter] public ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public ScrollOrientation? Orientation { get; set; }
        [Parameter] public ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(HorizontalScrollBarVisibility), (int)HorizontalScrollBarVisibility.Value);
            }
            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (VerticalScrollBarVisibility != null)
            {
                builder.AddAttribute(nameof(VerticalScrollBarVisibility), (int)VerticalScrollBarVisibility.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        private class BlazorScrollView : Xamarin.Forms.ScrollView, IFormsControlHandler
        {
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(HorizontalScrollBarVisibility):
                        HorizontalScrollBarVisibility = (ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(Orientation):
                        Orientation = (ScrollOrientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(VerticalScrollBarVisibility):
                        VerticalScrollBarVisibility = (ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
