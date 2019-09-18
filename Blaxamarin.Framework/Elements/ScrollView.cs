using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ScrollView : Element
    {
        static ScrollView()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<ScrollView, ScrollViewHandler>();
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
        [Parameter] public XF.ScrollBarVisibility? HorizontalScrollBarVisibility { get; set; }
        [Parameter] public XF.ScrollOrientation? Orientation { get; set; }
        [Parameter] public XF.ScrollBarVisibility? VerticalScrollBarVisibility { get; set; }

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

        private class ScrollViewHandler : IFormsControlHandler
        {
            public XF.ScrollView ScrollViewControl { get; set; } = new XF.ScrollView();
            public object NativeControl => ScrollViewControl;
            public XF.Element ElementControl => ScrollViewControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(HorizontalScrollBarVisibility):
                        ScrollViewControl.HorizontalScrollBarVisibility = (XF.ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(Orientation):
                        ScrollViewControl.Orientation = (XF.ScrollOrientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(VerticalScrollBarVisibility):
                        ScrollViewControl.VerticalScrollBarVisibility = (XF.ScrollBarVisibility)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        Element.ApplyAttribute(ScrollViewControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
