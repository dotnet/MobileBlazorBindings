using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class TextBox : FormsComponentBase
    {
        static TextBox()
        {
            BlontrolAdapter.KnownElements.Add(typeof(TextBox).FullName, new ComponentControlFactoryFunc<System.Windows.Forms.Control>((renderer, _) => new BlazorTextBox(renderer)));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool? Multiline { get; set; }
        [Parameter] public bool? ReadOnly { get; set; }
        [Parameter] public bool? WordWrap { get; set; }
        [Parameter] public ScrollBars? ScrollBars { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }
            if (Multiline != null)
            {
                builder.AddAttribute(nameof(Multiline), Multiline.Value);
            }
            if (ReadOnly != null)
            {
                builder.AddAttribute(nameof(ReadOnly), ReadOnly.Value);
            }
            if (WordWrap != null)
            {
                builder.AddAttribute(nameof(WordWrap), WordWrap.Value);
            }
            if (ScrollBars != null)
            {
                builder.AddAttribute(nameof(ScrollBars), (int)ScrollBars.Value);
            }
        }

        class BlazorTextBox : System.Windows.Forms.TextBox, IBlazorNativeControl
        {
            public BlazorTextBox(EmblazonRenderer<System.Windows.Forms.Control> renderer)
            {
            }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Text):
                        Text = (string)attributeValue;
                        break;
                    case nameof(Multiline):
                        Multiline = (bool)attributeValue;
                        break;
                    case nameof(ReadOnly):
                        ReadOnly = (bool)attributeValue;
                        break;
                    case nameof(WordWrap):
                        WordWrap = (bool)attributeValue;
                        break;
                    case nameof(ScrollBars):
                        ScrollBars = (ScrollBars)int.Parse((string)attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
