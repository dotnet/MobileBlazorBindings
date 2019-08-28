using Emblazon;
using Microsoft.AspNetCore.Components;
using System.Windows.Forms;

namespace BlinForms.Framework.Controls
{
    public class SplitContainer : FormsComponentBase
    {
        static SplitContainer()
        {
            NativeControlRegistry<System.Windows.Forms.Control>.RegisterNativeControlComponent<SplitContainer, BlazorSplitContainer>();
        }

        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public Orientation? Orientation { get; set; }
        [Parameter] public int? SplitterDistance { get; set; }
        
        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Orientation != null)
            {
                builder.AddAttribute(nameof(Orientation), (int)Orientation.Value);
            }
            if (SplitterDistance != null)
            {
                builder.AddAttribute(nameof(SplitterDistance), SplitterDistance.Value);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        class BlazorSplitContainer : System.Windows.Forms.SplitContainer, IBlazorNativeControl
        {
            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Orientation):
                        Orientation = (Orientation)AttributeHelper.GetInt(attributeValue);
                        break;
                    case nameof(SplitterDistance):
                        SplitterDistance = AttributeHelper.GetInt(attributeValue);
                        break;
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}
