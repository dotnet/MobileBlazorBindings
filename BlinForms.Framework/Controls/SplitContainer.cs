using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class SplitContainer : FormsComponentBase
    {
        static SplitContainer()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitContainer).FullName, renderer => new BlazorSplitContainer());
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public Orientation Orientation { get; set; }

        protected override void RenderAttributes(RenderTreeBuilder builder)
        {
            builder.AddAttribute(1, nameof(Orientation), (int)Orientation);
        }

        protected override void RenderContents(RenderTreeBuilder builder)
        {
            builder.AddContent(1000, ChildContent);
        }

        class BlazorSplitContainer : System.Windows.Forms.SplitContainer, IBlazorNativeControl
        {
            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Orientation):
                        if ((attributeValue as string) != "0")
                            Orientation = (Orientation)int.Parse((string)attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
