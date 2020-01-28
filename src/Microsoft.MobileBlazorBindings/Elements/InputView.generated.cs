// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;
using Microsoft.MobileBlazorBindings.Elements.Handlers;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public class InputView : View
    {
        [Parameter] public bool? IsReadOnly { get; set; }
        [Parameter] public bool? IsSpellCheckEnabled { get; set; }
        //[Parameter] public XF.Keyboard Keyboard { get; set; }
        [Parameter] public int? MaxLength { get; set; }

        public new XF.InputView NativeControl => ((InputViewHandler)ElementHandler).InputViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsReadOnly != null)
            {
                builder.AddAttribute(nameof(IsReadOnly), IsReadOnly);
            }
            if (IsSpellCheckEnabled != null)
            {
                builder.AddAttribute(nameof(IsSpellCheckEnabled), IsSpellCheckEnabled);
            }
            if (MaxLength != null)
            {
                builder.AddAttribute(nameof(MaxLength), MaxLength);
            }
        }
    }
}
