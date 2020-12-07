// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Image : View
    {
        static Image()
        {
            ElementHandlerRegistry.RegisterElementHandler<Image>(
                renderer => new ImageHandler(renderer, new XF.Image()));
        }

        /// <summary>
        /// Gets or sets the scaling mode for the image. This is a bindable property.
        /// </summary>
        /// <value>
        /// A <see cref="T:Xamarin.Forms.Aspect" /> representing the scaling mode of the image. Default is <see cref="F:Xamarin.Forms.Aspect.AspectFit" />.
        /// </value>
        [Parameter] public XF.Aspect? Aspect { get; set; }
        [Parameter] public bool? IsAnimationPlaying { get; set; }
        /// <summary>
        /// Gets or sets a Boolean value that, if <see langword="true" /> hints to the rendering engine that it may safely omit drawing visual elements behind the image.
        /// </summary>
        /// <value>
        /// The value of the opacity rendering hint.
        /// </value>
        [Parameter] public bool? IsOpaque { get; set; }
        /// <summary>
        /// Gets or sets the source of the image. This is a bindable property.
        /// </summary>
        /// <value>
        /// An <see cref="T:Xamarin.Forms.ImageSource" /> representing the image source. Default is null.
        /// </value>
        [Parameter] public XF.ImageSource Source { get; set; }

        public new XF.Image NativeControl => ((ImageHandler)ElementHandler).ImageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Aspect != null)
            {
                builder.AddAttribute(nameof(Aspect), (int)Aspect.Value);
            }
            if (IsAnimationPlaying != null)
            {
                builder.AddAttribute(nameof(IsAnimationPlaying), IsAnimationPlaying.Value);
            }
            if (IsOpaque != null)
            {
                builder.AddAttribute(nameof(IsOpaque), IsOpaque.Value);
            }
            if (Source != null)
            {
                builder.AddAttribute(nameof(Source), AttributeHelper.ObjectToDelegate(Source));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
