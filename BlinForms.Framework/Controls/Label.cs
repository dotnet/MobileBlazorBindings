using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;

namespace BlinForms.Framework.Controls
{
    public class Label : FormsComponentBase
    {
        static Label()
        {
            Blontrol.KnownElements.Add(typeof(Label).FullName, renderer => new BlazorLabel());
        }

        [Parameter] public string Text { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(1, nameof(Text), Text);
        }

        class BlazorLabel : System.Windows.Forms.Label, IBlazorNativeControl
        {
            public BlazorLabel()
            {
            }

            public void ApplyAttribute(ref RenderTreeFrame attribute)
            {
                switch (attribute.AttributeName)
                {
                    case nameof(Text):
                        Text = (string)attribute.AttributeValue;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, ref attribute);
                        break;
                }
            }
        }
    }
}
