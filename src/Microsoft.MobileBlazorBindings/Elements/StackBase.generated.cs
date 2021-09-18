// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MC = Microsoft.Maui.Controls;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;

namespace Microsoft.MobileBlazorBindings.Elements
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
