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
    public partial class View : VisualElement
    {
        static View()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public MC.LayoutOptions? HorizontalOptions { get; set; }
        [Parameter] public Thickness? Margin { get; set; }
        [Parameter] public MC.LayoutOptions? VerticalOptions { get; set; }

        public new MC.View NativeControl => ((ViewHandler)ElementHandler).ViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (HorizontalOptions != null)
            {
                builder.AddAttribute(nameof(HorizontalOptions), AttributeHelper.LayoutOptionsToString(HorizontalOptions.Value));
            }
            if (Margin != null)
            {
                builder.AddAttribute(nameof(Margin), AttributeHelper.ThicknessToString(Margin.Value));
            }
            if (VerticalOptions != null)
            {
                builder.AddAttribute(nameof(VerticalOptions), AttributeHelper.LayoutOptionsToString(VerticalOptions.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
