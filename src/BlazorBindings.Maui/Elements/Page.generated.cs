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
    public partial class Page : VisualElement
    {
        static Page()
        {
            ElementHandlerRegistry.RegisterElementHandler<Page>(
                renderer => new PageHandler(renderer, new MC.Page()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.ImageSource BackgroundImageSource { get; set; }
        [Parameter] public MC.ImageSource IconImageSource { get; set; }
        [Parameter] public bool? IsBusy { get; set; }
        [Parameter] public Thickness? Padding { get; set; }
        [Parameter] public string Title { get; set; }

        public new MC.Page NativeControl => ((PageHandler)ElementHandler).PageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (BackgroundImageSource != null)
            {
                builder.AddAttribute(nameof(BackgroundImageSource), AttributeHelper.ObjectToDelegate(BackgroundImageSource));
            }
            if (IconImageSource != null)
            {
                builder.AddAttribute(nameof(IconImageSource), AttributeHelper.ObjectToDelegate(IconImageSource));
            }
            if (IsBusy != null)
            {
                builder.AddAttribute(nameof(IsBusy), IsBusy.Value);
            }
            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }
            if (Title != null)
            {
                builder.AddAttribute(nameof(Title), Title);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
