// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Page : VisualElement
    {
        static Page()
        {
            ElementHandlerRegistry.RegisterElementHandler<Page>(
                renderer => new PageHandler(renderer, new XF.Page()));
        }

        [Parameter] public XF.ImageSource BackgroundImageSource { get; set; }
        [Parameter] public XF.ImageSource IconImageSource { get; set; }
        /// <summary>
        /// Marks the Page as busy. This will cause the platform specific global activity indicator to show a busy state.
        /// </summary>
        /// <value>
        /// A bool indicating if the Page is busy or not.
        /// </value>
        [Parameter] public bool? IsBusy { get; set; }
        /// <summary>
        /// The space between the content of the <see cref="T:Xamarin.Forms.Page" /> and it's border.
        /// </summary>
        [Parameter] public XF.Thickness? Padding { get; set; }
        /// <summary>
        /// The <see cref="T:Xamarin.Forms.Page" />'s title.
        /// </summary>
        [Parameter] public string Title { get; set; }

        public new XF.Page NativeControl => ((PageHandler)ElementHandler).PageControl;

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
    }
}
