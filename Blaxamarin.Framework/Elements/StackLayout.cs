using Emblazon;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class StackLayout : FormsComponentBase
    {
        static StackLayout()
        {
            BlelementAdapter.KnownElements.Add(typeof(StackLayout).FullName, new ComponentControlFactoryFunc<Element>((_, __) => new BlazorStackLayout()));
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override void RenderContents(RenderTreeBuilder builder)
        {
            builder.AddContent(1000, ChildContent);
        }

        class BlazorStackLayout : Xamarin.Forms.StackLayout, IBlazorNativeControl
        {
            public BlazorStackLayout()
            {
            }

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    default:
                        FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }

    }
}
