// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
#pragma warning disable CA1724 // Type name matches .NET Framework namespace; this type name matches a Xamarin.Forms type name
    public abstract class Layout : View
#pragma warning restore CA1724 // Type name matches .NET Framework namespace
    {
        [Parameter] public XF.Thickness? Padding { get; set; }

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (Padding != null)
            {
                builder.AddAttribute(nameof(Padding), AttributeHelper.ThicknessToString(Padding.Value));
            }
        }
    }
}
