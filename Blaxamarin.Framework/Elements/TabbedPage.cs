using Emblazon;
using System;
using Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class TabbedPage : Page
    {
        static TabbedPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<TabbedPage, BlazorTabbedPage>();
        }

        protected static void ApplyAttribute(Xamarin.Forms.TabbedPage tabbedPage, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (tabbedPage is null)
            {
                throw new ArgumentNullException(nameof(tabbedPage));
            }

            switch (attributeName)
            {
                default:
                    ApplyAttribute((Xamarin.Forms.Page)tabbedPage, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private class BlazorTabbedPage : Xamarin.Forms.TabbedPage, IFormsControlHandler
        {
            public object NativeControl => this;
            public Element Element => this;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                TabbedPage.ApplyAttribute(this, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
