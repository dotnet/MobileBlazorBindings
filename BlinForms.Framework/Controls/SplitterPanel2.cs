using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;

namespace BlinForms.Framework.Controls
{
    public class SplitterPanel2 : FormsComponentBase
    {
        static SplitterPanel2()
        {
            BlontrolAdapter.KnownElements.Add(typeof(SplitterPanel2).FullName, new ComponentControlFactoryFunc((_, __) => new PlaceholderControl() { State = "Panel2", }));
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void RenderContents(RenderTreeBuilder builder)
        {
            builder.AddContent(1000, ChildContent);
        }
    }
}
