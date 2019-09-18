using Emblazon;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    internal abstract class MasterDetailChildPageBase : Element
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }


        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }
        }

        protected override RenderFragment GetChildContent() => ChildContent;

        protected abstract class ContentPageHandler : IFormsControlHandler
        {
            public XF.ContentPage ContentPageControl { get; set; } = new XF.ContentPage();
            public XF.Element ElementControl => ContentPageControl;
            public object NativeControl => ContentPageControl;

            public void ApplyAttribute(ulong attributeEventHandlerId, string attributeName, object attributeValue, string attributeEventUpdatesAttributeName)
            {
                switch (attributeName)
                {
                    case nameof(Title):
                        ContentPageControl.Title = (string)attributeValue;
                        break;
                    default:
                        Element.ApplyAttribute(ContentPageControl, attributeEventHandlerId, attributeName, attributeValue, attributeEventUpdatesAttributeName);
                        break;
                }
            }
        }
    }
}