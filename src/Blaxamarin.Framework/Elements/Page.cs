using Blaxamarin.Framework.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Blaxamarin.Framework.Elements
{
    public class Page : VisualElement
    {
        static Page()
        {
            ElementHandlerRegistry<IXamarinFormsElementHandler>.RegisterElementHandler<Page>(
                renderer => new PageHandler(renderer, new XF.Page()));
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
    }
}
