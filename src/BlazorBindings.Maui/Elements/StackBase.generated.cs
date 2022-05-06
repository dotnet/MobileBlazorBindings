// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements
{
    public abstract partial class StackBase : Layout
    {
        static StackBase()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public double? Spacing { get; set; }

        public new MC.StackBase NativeControl => ((StackBaseHandler)ElementHandler).StackBaseControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Spacing != null)
            {
                builder.AddAttribute(nameof(Spacing), AttributeHelper.DoubleToString(Spacing.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
