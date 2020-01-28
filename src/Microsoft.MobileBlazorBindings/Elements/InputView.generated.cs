// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class InputView : View
    {

        [Parameter] public bool? IsReadOnly { get; set; }
        [Parameter] public bool? IsSpellCheckEnabled { get; set; }
        [Parameter] public int? MaxLength { get; set; }

        public new XF.InputView NativeControl => ((InputViewHandler)ElementHandler).InputViewControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (IsReadOnly != null)
            {
                builder.AddAttribute(nameof(IsReadOnly), IsReadOnly.Value);
            }
            if (IsSpellCheckEnabled != null)
            {
                builder.AddAttribute(nameof(IsSpellCheckEnabled), IsSpellCheckEnabled.Value);
            }
            if (MaxLength != null)
            {
                builder.AddAttribute(nameof(MaxLength), MaxLength.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
