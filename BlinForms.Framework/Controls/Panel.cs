using System.Drawing;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    public class Panel : FormsComponentBase
    {
        static Panel()
        {
            BlontrolAdapter.KnownElements.Add(typeof(Panel).FullName, renderer => new BlazorPanel(renderer));
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public bool Visible { get; set; }
        [Parameter] public Color BackColor { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(1, nameof(Visible), Visible);
            builder.AddAttribute(2, nameof(BackColor), BackColor.ToArgb());
        }

        protected override void RenderContents(RenderTreeBuilder builder)
        {
            builder.AddContent(999999, ChildContent);
        }

        class BlazorPanel : System.Windows.Forms.Panel, IBlazorNativeControl
        {
            public BlazorPanel(BlinFormsRenderer renderer)
            {
            }

            public void ApplyAttribute(ref RenderTreeFrame attribute)
            {
                switch (attribute.AttributeName)
                {
                    case nameof(Visible):
                        Visible = (bool)attribute.AttributeValue;
                        break;
                    case nameof(BackColor):
                        BackColor = Color.FromArgb(argb: (int)attribute.AttributeValue);
                        break;
                    //case "onclick":
                    //    ClickEventHandlerId = attribute.AttributeEventHandlerId;
                    //    break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, ref attribute);
                        break;
                }
            }
        }
    }
}
