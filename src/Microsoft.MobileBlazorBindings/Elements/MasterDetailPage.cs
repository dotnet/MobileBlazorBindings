// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.MobileBlazorBindings.Core;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class MasterDetailPage : Page
    {
        static MasterDetailPage()
        {
            ElementHandlerRegistry
                .RegisterElementHandler<MasterDetailPage>(renderer => new MasterDetailPageHandler(renderer, new XF.MasterDetailPage()));
        }

        [Parameter] public XF.MasterBehavior? MasterBehavior { get; set; }

        public new XF.MasterDetailPage NativeControl => ((MasterDetailPageHandler)ElementHandler).MasterDetailPageControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);

            if (MasterBehavior != null)
            {
                builder.AddAttribute(nameof(MasterBehavior), (int)MasterBehavior.Value);
            }

            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);
    }
}
