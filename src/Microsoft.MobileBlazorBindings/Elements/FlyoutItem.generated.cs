// Copyright (c) Microsoft Corporation.
// Licensed under the MIT license.

using Microsoft.AspNetCore.Components;
using Microsoft.MobileBlazorBindings.Core;
using Microsoft.MobileBlazorBindings.Elements.Handlers;
using System.Threading.Tasks;
using XF = Xamarin.Forms;

namespace Microsoft.MobileBlazorBindings.Elements
{
    public partial class FlyoutItem : ShellItem
    {
        static FlyoutItem()
        {
            ElementHandlerRegistry.RegisterElementHandler<FlyoutItem>(
                renderer => new FlyoutItemHandler(renderer, new XF.FlyoutItem()));

            RegisterAdditionalHandlers();
        }

        public new XF.FlyoutItem NativeControl => ((FlyoutItemHandler)ElementHandler).FlyoutItemControl;

        protected override void RenderAttributes(AttributesBuilder builder)
        {
            base.RenderAttributes(builder);


            RenderAdditionalAttributes(builder);
        }

        partial void RenderAdditionalAttributes(AttributesBuilder builder);

        static partial void RegisterAdditionalHandlers();
    }
}
