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
            builder.AddContent(1000, ChildContent);
        }

        class BlazorPanel : System.Windows.Forms.Panel, IBlazorNativeControl
        {
            public BlazorPanel(BlinFormsRenderer renderer)
            {
            }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Visible):
                        Visible = (bool)attributeValue;
                        break;
                    case nameof(BackColor):
                        BackColor = Color.FromArgb(argb: int.Parse((string)attributeValue));
                        break;
                    //case "onclick":
                    //    ClickEventHandlerId = attributeEventHandlerId;
                    //    break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
