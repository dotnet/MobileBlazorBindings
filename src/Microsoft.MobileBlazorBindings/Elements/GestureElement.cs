// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using System.Collections.Generic;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class GestureElement : Element
    {
        public GestureElement()
        {
            GestureRecognizers = new List<IGestureRecognizer>();
        }

        public IList<IGestureRecognizer> GestureRecognizers { get; }

        internal void Change()
        {
            StateHasChanged();
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder)
        {
            if (GestureRecognizers.Count > 0)
            {
                AttributeValueHolder del = new AttributeValueHolder(GetGestureRecognizers);
                builder.AddAttribute(nameof(GestureRecognizers), del);
            }
        }

        public void GetGestureRecognizers(out object value)
        {
            value = GestureRecognizers;
        }
    }
}
