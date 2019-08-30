using Emblazon;
using Microsoft.AspNetCore.Components;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ContentView : FormsComponentBase
    {
        static ContentView()
        {
            NativeControlRegistry<IFormsControlHandler>
                .RegisterNativeControlComponent<ContentView>(renderer => new BlazorContentView());
        }
        
        internal class BlazorContentView : Xamarin.Forms.ContentView, IFormsControlHandler
        {
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                FormsComponentBase.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
