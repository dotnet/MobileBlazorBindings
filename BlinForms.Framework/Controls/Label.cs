using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    public class Label : FormsComponentBase
    {
        static Label()
        {
            BlontrolAdapter.KnownElements.Add(typeof(Label).FullName, new ComponentControlFactoryFunc<System.Windows.Forms.Control>((_, __) => new BlazorLabel()));
        }

        [Parameter] public string Text { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
        }

        class BlazorLabel : System.Windows.Forms.Label, IBlazorNativeControl
        {
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
