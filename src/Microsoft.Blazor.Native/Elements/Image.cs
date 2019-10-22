using Microsoft.Blazor.Native.Elements.Handlers;
using Emblazon;
using Microsoft.AspNetCore.Components;
using System;
using XF = Xamarin.Forms;

namespace Microsoft.Blazor.Native.Elements
{
    public class Image : View
    {
        static Image()
        {
            ElementHandlerRegistry.RegisterElementHandler<Image>(
                renderer => new ImageHandler(renderer, new XF.Image()));
        }

        [Parameter] public XF.Aspect? Aspect { get; set; }
        [Parameter] public bool? IsOpaque { get; set; }
        [Parameter] public XF.ImageSource Source { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Aspect != null)
            {
                builder.AddAttribute(nameof(Aspect), (int)Aspect.Value);
            }
            if (IsOpaque != null)
            {
                builder.AddAttribute(nameof(IsOpaque), IsOpaque.Value);
            }
            if (Source != null)
            {
                switch (Source)
                {
                    case XF.FileImageSource fileImageSource:
                        {
                            builder.AddAttribute(nameof(Source) + "_AsFile", fileImageSource.File);
                        }
                        break;
                    default:
                        throw new NotSupportedException($"Unsupported {nameof(Source)} type: {Source.GetType().FullName}.");
                }
            }
        }
    }
}
