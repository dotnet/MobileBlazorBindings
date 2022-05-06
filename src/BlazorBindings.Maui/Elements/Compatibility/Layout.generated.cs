// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using MCC = Microsoft.Maui.Controls.Compatibility;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui;
using BlazorBindings.Core;
using BlazorBindings.Maui.Elements;
using BlazorBindings.Maui.Elements.Compatibility.Handlers;
using System.Threading.Tasks;

namespace BlazorBindings.Maui.Elements.Compatibility
{
    public abstract partial class Layout : BlazorBindings.Maui.Elements.View
    {
        static Layout()
        {
            RegisterAdditionalHandlers();
        }

        [Parameter] public bool? CascadeInputTransparent { get; set; }
        [Parameter] public bool? IsClippedToBounds { get; set; }
        [Parameter] public Thickness? Padding { get; set; }

        public new MCC.Layout NativeControl => ((LayoutHandler)ElementHandler).LayoutControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (CascadeInputTransparent != null)
            {
                builder.AddAttribute(nameof(CascadeInputTransparent), CascadeInputTransparent.Value);
            }
            if (IsClippedToBounds != null)
            {
                builder.AddAttribute(nameof(IsClippedToBounds), IsClippedToBounds.Value);
            }
            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
