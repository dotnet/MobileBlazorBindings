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
    public partial class ProgressBar : View
    {
        static ProgressBar()
        {
            ElementHandlerRegistry.RegisterElementHandler<ProgressBar>(
                renderer => new ProgressBarHandler(renderer, new MC.ProgressBar()));

            RegisterAdditionalHandlers();
        }

        [Parameter] public double? Progress { get; set; }
        [Parameter] public Color ProgressColor { get; set; }

        public new MC.ProgressBar NativeControl => ((ProgressBarHandler)ElementHandler).ProgressBarControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Progress != null)
            {
                builder.AddAttribute(nameof(Progress), AttributeHelper.DoubleToString(Progress.Value));
            }
            if (ProgressColor != null)
            {
                builder.AddAttribute(nameof(ProgressColor), AttributeHelper.ColorToString(ProgressColor));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
