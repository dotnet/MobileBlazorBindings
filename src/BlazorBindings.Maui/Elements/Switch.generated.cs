// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Graphics;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public partial class Switch : View
    {
        static Switch()
        {
            ElementHandlerRegistry.RegisterElementHandler<Switch>(
                renderer => new SwitchHandler(renderer, new MC.Switch()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public bool? IsToggled { get; set; }
        [Parameter] public Color OnColor { get; set; }
        [Parameter] public Color ThumbColor { get; set; }

        public new MC.Switch NativeControl => ((SwitchHandler)ElementHandler).SwitchControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsToggled != null)
            {
                builder.AddAttribute(nameof(IsToggled), IsToggled.Value);
            }
            if (OnColor != null)
            {
                builder.AddAttribute(nameof(OnColor), AttributeHelper.ColorToString(OnColor));
            }
            if (ThumbColor != null)
            {
                builder.AddAttribute(nameof(ThumbColor), AttributeHelper.ColorToString(ThumbColor));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
