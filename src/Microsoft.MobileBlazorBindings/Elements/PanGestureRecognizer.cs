// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class PanGestureRecognizer : GestureRecognizer
    {
        static PanGestureRecognizer()
        {
            ElementHandlerRegistry.RegisterElementHandler<PanGestureRecognizer>(
                renderer => new PanGestureRecognizerHandler(renderer, new XF.PanGestureRecognizer()));
        }

        [Parameter] public EventCallback<XF.PanUpdatedEventArgs> OnPanUpdated { get; set; }

        public new XF.PanGestureRecognizer NativeControl => ((PanGestureRecognizerHandler)ElementHandler).PanGestureRecognizerControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            builder.AddAttribute("onpanupdated", OnPanUpdated);
        }
    }
}
