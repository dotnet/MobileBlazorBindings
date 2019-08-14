using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class TextBox : FormsComponentBase
    {
        static TextBox()
        {
            BlontrolAdapter.KnownElements.Add(typeof(TextBox).FullName, renderer => new BlazorTextBox(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public bool Multiline { get; set; }
        [Parameter] public bool ReadOnly { get; set; }
        [Parameter] public bool WordWrap { get; set; }
        [Parameter] public ScrollBars ScrollBars { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(1, nameof(Text), Text);
            builder.AddAttribute(2, nameof(Multiline), Multiline);
            builder.AddAttribute(3, nameof(ReadOnly), ReadOnly);
            builder.AddAttribute(4, nameof(WordWrap), WordWrap);
            builder.AddAttribute(5, nameof(ScrollBars), (int)ScrollBars);
        }

        class BlazorTextBox : System.Windows.Forms.TextBox, IBlazorNativeControl
        {
            public BlazorTextBox(BlinFormsRenderer renderer)
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
                        if ((attributeValue as string) != "0")
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
