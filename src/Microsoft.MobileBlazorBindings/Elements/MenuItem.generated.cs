// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class MenuItem : BaseMenuItem
    {
        static MenuItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<MenuItem>(
                renderer => new MenuItemHandler(renderer, new XF.MenuItem()));
        }

        [Parameter] public string @class { get; set; }
        [Parameter] public XF.ImageSource IconImageSource { get; set; }
        /// <summary>
        /// Gets or sets a value that indicates whether or not the menu item removes its associated UI element.
        /// </summary>
        /// <value>
        /// False
        /// </value>
        [Parameter] public bool? IsDestructive { get; set; }
        /// <summary>
        /// For internal use by the Xamarin.Forms platform.
        /// </summary>
        [Parameter] public bool? IsEnabled { get; set; }
        /// <summary>
        /// Sets the StyleClass of the generated <see cref="T:Xamarin.Forms.FlyoutItem" /> when used with <see cref="T:Xamarin.Forms.Shell" />
        /// </summary>
        [Parameter] public string StyleClass { get; set; }
        /// <summary>
        /// The text of the menu item.
        /// </summary>
        [Parameter] public string Text { get; set; }

        public new XF.MenuItem NativeControl => ((MenuItemHandler)ElementHandler).MenuItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (@class != null)
            {
                builder.AddAttribute(nameof(@class), @class);
            }
            if (IconImageSource != null)
            {
                builder.AddAttribute(nameof(IconImageSource), AttributeHelper.ObjectToDelegate(IconImageSource));
            }
            if (IsDestructive != null)
            {
                builder.AddAttribute(nameof(IsDestructive), IsDestructive.Value);
            }
            if (IsEnabled != null)
            {
                builder.AddAttribute(nameof(IsEnabled), IsEnabled.Value);
            }
            if (StyleClass != null)
            {
                builder.AddAttribute(nameof(StyleClass), StyleClass);
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
