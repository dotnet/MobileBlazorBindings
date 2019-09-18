using Emblazon;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Page : Element
    {
        static Page()
        {
            NativeControlRegistry<IFormsControlHandler>.RegisterNativeControlComponent<Page, PageHandler>();
        }

#pragma warning disable CA1721 // Property names should not match get methods
        [Parameter] public RenderFragment ChildContent { get; set; }
#pragma warning restore CA1721 // Property names should not match get methods
        [Parameter] public XF.ImageSource IconImageSource { get; set; }
        [Parameter] public string Title { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IconImageSource != null)
            {
                switch (IconImageSource)
                {
                    case XF.FileImageSource fileImageSource:
                        {
                            builder.AddAttribute(nameof(IconImageSource) + "_AsFile", fileImageSource.File);
                        }
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported {nameof(IconImageSource)} type: {IconImageSource.GetType().FullName}.");
                }
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        protected static void ApplyAttribute(Xamarin.Forms.Page page, ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
        {
            if (page is null)
            {
                throw new ArgumentNullException(nameof(page));
            }

            switch (attributeName)
            {
                case nameof(IconImageSource) + "_AsFile":
                    page.IconImageSource = new XF.FileImageSource { File = (string)attributeValue };
                    break;
                case nameof(Title):
                    page.Title = (string)attributeValue;
                    break;
                default:
                    Element.ApplyAttribute(page, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                    break;
            }
        }

        private class PageHandler : IFormsControlHandler
        {
            public XF.Page PageControl { get; set; } = new XF.Page();
            public object NativeControl => PageControl;
            public XF.Element ElementControl => PageControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                Page.ApplyAttribute(PageControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
            }
        }
    }
}
