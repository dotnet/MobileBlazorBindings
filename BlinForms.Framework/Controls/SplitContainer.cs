using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class SplitContainer : FormsComponentBase
    {
        static SplitContainer()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitContainer).FullName, new ComponentControlFactoryFunc<System.Windows.Forms.Control>((_, __) => new BlazorSplitContainer()));
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public Orientation? Orientation { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
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
