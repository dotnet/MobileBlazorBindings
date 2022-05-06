// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class MenuItem : BaseMenuItem
    {
        static MenuItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<MenuItem>(
                renderer => new MenuItemHandler(renderer, new MC.MenuItem()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public string @class { get; set; }
        [Parameter] public MC.ImageSource IconImageSource { get; set; }
        [Parameter] public bool? IsDestructive { get; set; }
        [Parameter] public bool? IsEnabled { get; set; }
        [Parameter] public string StyleClass { get; set; }
        [Parameter] public string Text { get; set; }

        public new MC.MenuItem NativeControl => ((MenuItemHandler)ElementHandler).MenuItemControl;

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

        static partial void RegisterAdditionalHandlers();
    }
}
