using System;
using System.Drawing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    public class Button : FormsComponentBase
    {
        static Button()
        {
            Blontrol.KnownElements.Add(typeof(Button).FullName, renderer => new BlazorButton(renderer));
        }

        [Parameter] public string Text { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(1, nameof(Text), Text);
            builder.AddAttribute(2, "onclick", OnClick);
        }

        class BlazorButton : System.Windows.Forms.Button, IBlazorNativeControl
        {
            public ulong ClickEventHandlerId { get; set; }

            public BlazorButton(BlinFormsRenderer renderer)
            {
                Click += (s, e) =>
                {
                    if (ClickEventHandlerId != default)
                    {
                        renderer.DispatchEventAsync(ClickEventHandlerId, null, new UIEventArgs());
                    }
                };
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
                    case "onclick":
                        ClickEventHandlerId = attribute.AttributeEventHandlerId;
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, ref attribute);
                        break;
                }
            }
        }
    }
}
