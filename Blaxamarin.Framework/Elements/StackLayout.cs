using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class StackLayout : Element
    {
        static StackLayout()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<StackLayout, StackLayoutHandler>();
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
        [Parameter] public XF.StackOrientation? Orientation { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        private class StackLayoutHandler : IFormsControlHandler
        {
            public XF.StackLayout StackLayoutControl { get; set; } = new XF.StackLayout();
            public object NativeControl => StackLayoutControl;
            public XF.Element ElementControl => StackLayoutControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Orientation):
                        StackLayoutControl.Orientation = (XF.StackOrientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        Element.ApplyAttribute(StackLayoutControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
