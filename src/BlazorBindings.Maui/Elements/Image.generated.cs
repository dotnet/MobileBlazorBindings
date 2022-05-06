// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Image : View
    {
        static Image()
        {
            ElementHandlerRegistry.RegisterElementHandler<Image>(
                renderer => new ImageHandler(renderer, new MC.Image()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Aspect? Aspect { get; set; }
        [Parameter] public bool? IsAnimationPlaying { get; set; }
        [Parameter] public bool? IsOpaque { get; set; }
        [Parameter] public MC.ImageSource Source { get; set; }

        public new MC.Image NativeControl => ((ImageHandler)ElementHandler).ImageControl;

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

        static partial void RegisterAdditionalHandlers();
    }
}
