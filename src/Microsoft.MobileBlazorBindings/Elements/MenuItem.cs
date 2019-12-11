// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class MenuItem : BaseMenuItem
    {
        static MenuItem()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<MenuItem>(renderer => new MenuItemHandler(renderer, new XF.MenuItem()));
        }

        //[Parameter] public ICommand Command { get; set; }
        //[Parameter] public object CommandParameter { get; set; }
        [Parameter] public XF.ImageSource IconImageSource { get; set; }
        [Parameter] public bool? IsDestructive { get; set; }
        [Parameter] public string Text { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IconImageSource != null)
            {
                builder.AddAttribute(nameof(IconImageSource), AttributeHelper.ImageSourceToString(IconImageSource));
            }
            if (IsDestructive != null)
            {
                builder.AddAttribute(nameof(IsDestructive), IsDestructive.Value);
            }
            if (Text != null)
            {
                builder.AddAttribute(nameof(Text), Text);
            }

            builder.AddAttribute("onclick", OnClick);
        }
    }
}
