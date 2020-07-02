// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public abstract class Element : NativeControlComponentBase
    {
        [Parameter] public string AutomationId { get; set; }
        [Parameter] public string ClassId { get; set; }
        [Parameter] public string StyleId { get; set; }

        public XF.Element NativeControl => ((Handlers.ElementHandler)ElementHandler).ElementControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (AutomationId != null)
            {
                builder.AddAttribute(nameof(AutomationId), AutomationId);
            }
            if (ClassId != null)
            {
                builder.AddAttribute(nameof(ClassId), ClassId);
            }
            if (StyleId != null)
            {
                builder.AddAttribute(nameof(StyleId), StyleId);
            }
        }
    }
}
