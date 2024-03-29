// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using MC = Microsoft.Maui.Controls;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TapGestureRecognizer : GestureRecognizer
    {
        static TapGestureRecognizer()
        {
            ElementHandlerRegistry.RegisterElementHandler<TapGestureRecognizer>(
                renderer => new TapGestureRecognizerHandler(renderer, new MC.TapGestureRecognizer()));
        }

        [Parameter] public int? NumberOfTapsRequired { get; set; }

        [Parameter] public EventCallback OnTapped { get; set; }

        public new MC.TapGestureRecognizer NativeControl => ((TapGestureRecognizerHandler)ElementHandler).TapGestureRecognizerControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (NumberOfTapsRequired != null)
            {
                builder.AddAttribute(nameof(NumberOfTapsRequired), NumberOfTapsRequired.Value);
            }

            builder.AddAttribute("ontapped", OnTapped);
        }
    }
}
