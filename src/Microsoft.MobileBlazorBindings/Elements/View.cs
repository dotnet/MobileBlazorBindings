// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class View : VisualElement
    {
#pragma warning disable CA2227 // Collection properties should be read only; needs to be settable in markup
        [Parameter] public IList<XF.IGestureRecognizer> GestureRecognizers { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (GestureRecognizers?.Count > 0)
            {
                builder.AddAttribute(nameof(GestureRecognizers), new AttributeValueHolder(GetGestureRecognizers));
            }
        }

        private void GetGestureRecognizers(out object value)
        {
            value = GestureRecognizers;
        }
    }
}
