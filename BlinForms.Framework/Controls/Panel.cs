using System.Windows.Forms;
using Emblazon;
using Microsoft.AspNetCore.Components;

namespace BlinForms.Framework.Controls
{
    public class Panel : FormsComponentBase
    {
        static Panel()
        {
            NativeControlRegistry<IWindowsFormsControlHandler>.RegisterNativeControlComponent<Panel, BlazorPanel>();
        }

        [Parameter] public bool? AutoScroll { get; set; }
#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutoScroll != null)
            {
                builder.AddAttribute(nameof(AutoScroll), AutoScroll.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        class BlazorPanel : System.Windows.Forms.Panel, IWindowsFormsControlHandler
        {
            public Control Control => this;
            public object NativeControl => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(AutoScroll):
                        AutoScroll = AttributeHelper.GetBool(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
