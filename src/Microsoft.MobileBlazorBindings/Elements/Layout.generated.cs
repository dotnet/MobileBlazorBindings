// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract partial class Layout : View
    {

        [Parameter] public bool? CascadeInputTransparent { get; set; }
        [Parameter] public bool? IsClippedToBounds { get; set; }
        [Parameter] public XF.Thickness? Padding { get; set; }

        public new XF.Layout NativeControl => ((LayoutHandler)ElementHandler).LayoutControl;

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
    }
}
