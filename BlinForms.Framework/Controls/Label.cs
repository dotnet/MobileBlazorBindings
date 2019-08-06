using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Drawing;

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

            protected override void OnParentChanged(EventArgs e)
            {
                base.OnParentChanged(e);

                if (Parent != null)
                {
                    Parent.Location = Location;
                    Location = Point.Empty;
                    Parent.Size = Size;
                }
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);

                if (Parent != null)
                {
                    Parent.Location = Location;
                    Location = Point.Empty;
                    Parent.Size = Size;
                }
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
