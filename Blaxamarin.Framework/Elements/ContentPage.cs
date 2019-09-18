using Emblazon;
using System;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ContentPage : Page
    {
        static ContentPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<ContentPage, BlazorContentPage>();
        }

        protected static void ApplyAttribute(Xamarin.Forms.ContentPage contentPage, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (contentPage is null)
            {
                throw new ArgumentNullException(nameof(contentPage));
            }

            switch (attributeName)
            {
                default:
                    ApplyAttribute((Xamarin.Forms.Page)contentPage, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private class BlazorContentPage : Xamarin.Forms.ContentPage, IFormsControlHandler
        {
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                ContentPage.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
