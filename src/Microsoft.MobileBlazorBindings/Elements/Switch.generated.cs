// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class Switch : View
    {
        static Switch()
        {
            ElementHandlerRegistry.RegisterElementHandler<Switch>(
                renderer => new SwitchHandler(renderer, new XF.Switch()));
        }

        [Parameter] public bool? IsToggled { get; set; }

        public new XF.Switch NativeControl => ((SwitchHandler)ElementHandler).SwitchControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsToggled != null)
            {
                builder.AddAttribute(nameof(IsToggled), IsToggled.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
