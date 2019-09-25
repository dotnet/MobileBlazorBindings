using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class Label : FormsComponentBase
    {
        static Label()
        {
            ElementHandlerRegistry<IWindowsFormsControlHandler>.RegisterElementHandler<Label, BlazorLabel>();
        }

        [Parameter] public string Text { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
        }

        private class BlazorLabel : System.Windows.Forms.Label, IWindowsFormsControlHandler
        {
            public Control Control => this;
            public object NativeControl => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
