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
    public partial class ActivityIndicator : View
    {
        static ActivityIndicator()
        {
            ElementHandlerRegistry.RegisterElementHandler<ActivityIndicator>(
                renderer => new ActivityIndicatorHandler(renderer, new MC.ActivityIndicator()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public Color Color { get; set; }
        [Parameter] public bool? IsRunning { get; set; }

        public new MC.ActivityIndicator NativeControl => ((ActivityIndicatorHandler)ElementHandler).ActivityIndicatorControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Color != null)
            {
                builder.AddAttribute(nameof(Color), AttributeHelper.ColorToString(Color));
            }
            if (IsRunning != null)
            {
                builder.AddAttribute(nameof(IsRunning), IsRunning.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
