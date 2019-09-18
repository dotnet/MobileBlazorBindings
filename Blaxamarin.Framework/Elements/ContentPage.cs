using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class ContentPage : Page
    {
        static ContentPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<ContentPage, ContentPageHandler>();
        }

        protected static void ApplyAttribute(XF.ContentPage contentPage, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (contentPage is null)
            {
                throw new ArgumentNullException(nameof(contentPage));
            }

            switch (attributeName)
            {
                default:
                    ApplyAttribute((XF.Page)contentPage, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private class ContentPageHandler : IFormsControlHandler
        {
            public XF.ContentPage ContentPageControl { get; set; } = new XF.ContentPage();
            public object NativeControl => ContentPageControl;
            public XF.Element ElementControl => ContentPageControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                ContentPage.ApplyAttribute(ContentPageControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
