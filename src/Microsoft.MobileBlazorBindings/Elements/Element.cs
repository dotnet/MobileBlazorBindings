// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class Element : NativeControlComponentBase
    {
        [Parameter] public string StyleId { get; set; }

        public XF.Element NativeControl => ((Handlers.ElementHandler)ElementHandler).ElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (StyleId != null)
            {
                builder.AddAttribute(nameof(StyleId), StyleId);
            }
        }
    }
}
