using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel1 : FormsComponentBase
    {
        static SplitterPanel1()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitterPanel1).FullName, _ => new PlaceholderControl() { State = "Panel1", });
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void RenderContents(RenderTreeBuilder builder)
        {
            builder.AddContent(1000, ChildContent);
        }
    }
}
