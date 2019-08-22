using Emblazon;
using Microsoft.AspNetCore.Components;

namespace Blaxamarin.Framework.Elements
{
    public class StackLayout : FormsComponentBase
    {
        static StackLayout()
        {
            BlelementAdapter.RegisterNativeControlComponent<StackLayout, BlazorStackLayout>();
        }

        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override RenderFragment GetChildContent() => ChildContent;

        class BlazorStackLayout : Xamarin.Forms.StackLayout, IBlazorNativeControl
        {
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
