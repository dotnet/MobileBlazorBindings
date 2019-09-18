using Emblazon;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class TabbedPage : Page
    {
        static TabbedPage()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<TabbedPage, TabbedPageHandler>();
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
                    ApplyAttribute((XF.Page)tabbedPage, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private class TabbedPageHandler : IFormsControlHandler
        {
            public XF.TabbedPage TabbedPageControl { get; set; } = new XF.TabbedPage();
            public object NativeControl => TabbedPageControl;
            public XF.Element ElementControl => TabbedPageControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                TabbedPage.ApplyAttribute(TabbedPageControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
