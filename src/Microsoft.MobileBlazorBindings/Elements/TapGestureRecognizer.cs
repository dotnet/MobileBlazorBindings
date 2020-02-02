// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class TapGestureRecognizer : GestureRecognizer
    {
        static TapGestureRecognizer()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<TapGestureRecognizer>(renderer => new TapGestureRecognizerHandler(renderer, new XF.TapGestureRecognizer()));
        }

        [Parameter] public EventCallback OnTapped { get; set; }

        [Parameter] public int? NumberOfTapsRequired { get; set; }


        [CascadingParameter(Name = "GestureElement")]
        public GestureElement GestureElement { get; set; }

        protected override void OnParametersSet()
        {
            if (!GestureElement.GestureRecognizers.Contains(this))
            {
                GestureElement.GestureRecognizers.Add(this);
                
                GestureElement.Change();
            }

        }

        public new XF.TapGestureRecognizer NativeControl => ((TapGestureRecognizerHandler)ElementHandler).TapGestureRecognizerControl;

        public override XF.IGestureRecognizer GestureRecognizerControl => NativeControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (NumberOfTapsRequired.HasValue)
            {
                builder.AddAttribute(nameof(NumberOfTapsRequired), NumberOfTapsRequired);
            }
            if (OnTapped.HasDelegate)
            {
                builder.AddAttribute("ontapped", OnTapped);
            }
        }
    }
}
