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

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
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
