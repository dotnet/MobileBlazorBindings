using Emblazon;
using Microsoft.AspNetCore.Components;

namespace BlinForms.Framework.Controls
{
    public class Panel : FormsComponentBase
    {
        static Panel()
        {
            NativeControlRegistry<System.Windows.Forms.Control>.RegisterNativeControlComponent<Panel, BlazorPanel>();
        }

        [Parameter] public bool? AutoScroll { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutoScroll != null)
            {
                builder.AddAttribute(nameof(AutoScroll), AutoScroll.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        class BlazorPanel : System.Windows.Forms.Panel, IBlazorNativeControl
        {
            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(AutoScroll):
                        AutoScroll = (bool)attributeValue;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
